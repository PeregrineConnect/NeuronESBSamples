using System;
using Neuron.NetX;

namespace Neuron.EnterpriseServiceBus.Samples
{
    public class Receiver
    {
        static int messageCount = 0;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Initializing receiver3");

                using (Subscriber subscriber = new Subscriber())
                {
                    subscriber.OnReceive += OnReceive;
                    var retValue = subscriber.Connect();
                    if (retValue != null && retValue.Count > 0)
                    {
                        foreach (var error in retValue.GetResults())
                        {
                            Console.WriteLine(error.Exception.ToString());
                        }
                    }

                    Console.WriteLine("Ready to receive");
                    Console.ReadLine();
                    Console.WriteLine("Press <ENTER> to shut down.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                Console.WriteLine("Press <ENTER> to shut down.");
                Console.ReadLine();
            }
        }

        public static void OnReceive(object sender, MessageEventArgs e)
        {
            messageCount++;
            Console.WriteLine("Received messsage " + messageCount.ToString());

            Console.WriteLine(e.Message.ToXml());
        }
    }

}
