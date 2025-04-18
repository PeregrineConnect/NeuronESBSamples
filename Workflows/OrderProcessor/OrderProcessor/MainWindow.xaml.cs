﻿using Neuron.NetX;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace OrderProcessor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly Subscriber subscriber = new Subscriber();
		readonly ObservableCollection<Order> orders = new ObservableCollection<Order>();

		public MainWindow()
		{
			InitializeComponent();
			//this.Loaded += Window_Loaded;
			this.Unloaded += MainWindow_Unloaded;
		}

		void MainWindow_Unloaded(object sender, RoutedEventArgs e)
		{
			subscriber.Dispose();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ordersList.ItemsSource = orders;

			subscriber.OnReceive += subscriber_OnReceive;
			PartyConnectExceptions exceptions = subscriber.Connect();
			if (exceptions != null && exceptions.Count > 0)
			{
				foreach (var ex in exceptions.GetResults())
					Console.WriteLine(string.Format("An error occurred connecting '{0}' to Topic, '{1}'. {2}", subscriber.Context.PartyId, ex.Exception.Message, ex.Topic));
			}
			else
			{
				Console.WriteLine($"Connected !");
			}
		}

		void subscriber_OnReceive(object o, MessageEventArgs e)
		{
			var xml = e.Message.ToXDocument();

			if (xml != null)
			{
				string orderID = xml.Root.Element("OrderID").Value;
				string batchID = e.Message.GetProperty("orders", "batchID");

				orders.Add(new Order() { BatchID = batchID, OrderID = orderID, Message = e.Message });
			}
		}

		private void SendOrder(string status)
		{
			if (ordersList.SelectedItem == null)
			{
				MessageBox.Show("Select an order");
				return;
			}
			var selectedOrder = (Order)ordersList.SelectedItem;

			var message = (ESBMessage)selectedOrder.Message.Clone();
			message.Header.Topic = "Orders.Processed";

			var xml = message.ToXDocument();
			xml.Root.Add(new XElement("Status", status));
			message.FromXml(xml.ToString());

			using (var publisher = new Publisher())
			{
				publisher.Connect();
				//publisher.SendMessage(message);

				// Run publisher on a separate thread so that it does block the main thread
				Task.Run(() => { message = publisher.SendMessage(message); }).GetAwaiter().GetResult();

			}

			orders.Remove(selectedOrder);
		}

		private void CompleteOrder_Click(object sender, RoutedEventArgs e)
		{
			SendOrder("Completed");
		}

		private void BackOrder_Click(object sender, RoutedEventArgs e)
		{
			SendOrder("BackOrder");
		}

		private void Discontinued_Click(object sender, RoutedEventArgs e)
		{
			SendOrder("Discontinued");
		}

		private void Other_Click(object sender, RoutedEventArgs e)
		{
			SendOrder("Other");
		}
	}
}