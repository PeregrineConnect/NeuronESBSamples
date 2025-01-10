using Neuron.NetX.Pipelines;
using Neuron.NetX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProcessSteps
{
	internal class PipelineTestBehavior<T> : PipelineRuntimeBehavior
	{
		//private static readonly ILog Log = log4net.LogManager.GetLogger(typeof(PipelineTestBehavior<T>));
		private string childstepName = "";
		private string stepName = "";

		protected override void OnAttach(PipelineRuntime owner)
		{
			base.OnAttach(owner);
			owner.InstanceCreated += owner_InstanceCreated;
			owner.InstanceCompleted += owner_InstanceCompleted;
			owner.InstanceAborted += owner_InstanceAborted;
		}

		private void owner_InstanceCreated(object sender, PipelineInstanceEventArgs e)
		{
			e.Instance.TraceAdded += Instance_TraceAdded;

			if (string.IsNullOrEmpty(stepName))
			{
				Console.Error.WriteLine("CREATED");
				stepName = ((PipelineInstance<ESBMessage>)(e.Instance)).Pipeline.Name;
			}
			else
			{
				if (string.IsNullOrEmpty(childstepName))
				{
					childstepName = ((PipelineInstance<ESBMessage>)(e.Instance)).Pipeline.Name;
				}
			}

			if (e.Instance.Runtime.DesignMode)
			{
				PipelineInstance<T>.DesignerStepDelegate stepChangedDelegate = this.Instance_StepChanged;
				PipelineInstance<T>.DesignerStepDelegate stepStartingDelegate = this.Instance_StepStarting;
				PipelineInstance<T>.DesignerStepDelegate stepEndedDelegate = this.Instance_StepEnded;
				PipelineInstance<T>.DesignerTraceDelegate traceAddedDelegate = this.Instance_TraceAdded;

				var pipeInstance = (e.Instance as PipelineInstance<T>);
				pipeInstance.SetDelegates(stepStartingDelegate, stepChangedDelegate, stepEndedDelegate, traceAddedDelegate);

				e.Instance.StepChanged += this.Instance_StepChanged;
				e.Instance.StepStarting += this.Instance_StepStarting;
				e.Instance.StepEnded += this.Instance_StepEnded;
			}
		}

		private void owner_InstanceCompleted(object sender, PipelineInstanceEventArgs e)
		{
			var instance = ((PipelineInstance<ESBMessage>)(e.Instance));

			if (instance.Context.Properties.ContainsKey("PipelineException"))
			{
				string mess =
					((Exception)(((PipelineInstance<ESBMessage>)(e.Instance)).Context.Properties["PipelineException"]))
						.InnerException.StackTrace;

				//Log.DebugFormat(CultureInfo.CurrentCulture, "Error = {0}", mess);
				Console.Error.WriteLine("TRACEADDED#" + TraceLevel.Error + "|" + mess);
			}

			if (childstepName == ((PipelineInstance<ESBMessage>)(e.Instance)).Pipeline.Name)
			{
				childstepName = string.Empty;
			}
			else if (stepName == ((PipelineInstance<ESBMessage>)(e.Instance)).Pipeline.Name)
			{
				Console.Error.WriteLine("COMPLETED");
				stepName = string.Empty;
			}
			e.Instance.TraceAdded -= Instance_TraceAdded;
			e.Instance.StepStarting -= this.Instance_StepStarting;
			e.Instance.StepEnded -= this.Instance_StepEnded;
			e.Instance.StepChanged -= Instance_StepChanged;
		}

		private void owner_InstanceAborted(object sender, PipelineInstanceEventArgs e)
		{
			var instance = ((PipelineInstance<ESBMessage>)(e.Instance));

			if (instance.Context.Properties.ContainsKey("PipelineException"))
			{
				string mess =
					((Exception)(((PipelineInstance<ESBMessage>)(e.Instance)).Context.Properties["PipelineException"]))
						.InnerException.StackTrace;

				//Log.DebugFormat(CultureInfo.CurrentCulture, "Error = {0}", mess);
				Console.Error.WriteLine("TRACEADDED#" + TraceLevel.Error + "|" + mess);
			}
		}


		private void Instance_StepStarting(object sender, EventArgs e)
		{
			var instance = (PipelineInstance<ESBMessage>)sender;

			instance.TraceInformation("Step input message...\n" + instance.Context.Data.ToString());
		}

		private void Instance_StepEnded(object sender, EventArgs e)
		{
			var instance = (PipelineInstance<ESBMessage>)sender;

			instance.TraceInformation("Step output message...\n" + instance.Context.Data.ToString());
		}

		private void Instance_TraceAdded(object sender, TraceEventArgs e)
		{
			Console.Error.WriteLine("TRACEADDED#" + e.TraceLevel + "|" + e.StepName + " | " + e.Message);
		}

		private void Instance_StepChanged(object sender, EventArgs e)
		{
			var senderType = (PipelineInstance<ESBMessage>)(sender);
			//Log.Debug("Instance Changed : " + senderType.CurrentStep.Uniquename);
			if (!string.IsNullOrEmpty(childstepName))
			{
				Console.Error.WriteLine("CHILDSTEPCHANGED#" + senderType.CurrentStep);
			}
			else
			{
				if (!string.IsNullOrEmpty(senderType.CurrentStep.Uniquename))
				{
					Console.Error.WriteLine("STEPCHANGED#" + senderType.CurrentStep.Uniquename);
				}
			}
		}
	}
}
