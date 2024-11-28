


Connect()


Sub Connect()

    'Create an instance of a subscriber and connect to NetX.
    'This uses WScript.CreateObject to connect to the events. Alternatively, 
    '    CreateObject("Neuron.NetX.Excel.Interop.Party")
    ' can be used, instead of the WScript.CreateObject from WScript

    
    Dim o
    'Set o = CreateObject("Neuron.NetX.Excel.Interop.Party")
    'WScript.ConnectObject o,"Neuron_"
    Set o = WScript.CreateObject("Neuron.NetX.Excel.Interop.Party","Neuron_")
    
    ' Connect to the bus
    o.Connect "FinancePublisher", "Enterprise", "localhost", "50000"

    Wscript.Sleep 200

    ' Send a test message
    o.Send "Finance", "<xml>hi from vbscript</xml>"
    MsgBox "Sent Message"

    ' Wait to receive a message, if one is sent
    o.StartReceive
    Wscript.Sleep 15000

    'Stop receiving and Disconnect from the bus
    o.StopReceive
    o.Disconnect

End Sub


' Below are all the events. All Defined events on the Neuron Party
' must be preceded with "Neuron_" to be recognized by the VBScript Parser

Sub Neuron_OnConnect(partyId, zone, server, port)
 
    	MsgBox "Connected Event: " & partyId
End Sub

Sub Neuron_OnDisconnect(partyId)

    	MsgBox "Disconnected Event:  " & partyId
End Sub

Sub Neuron_OnMessageReceive(message)

    	MsgBox "Receive Event:  " & message
End Sub

