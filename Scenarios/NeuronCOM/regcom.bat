@echo off


regasm bin\Release\Neuron.NetX.Excel.Interop.dll /u
regasm bin\Release\Neuron.NetX.Excel.Interop.dll /tlb:Neuron.NetX.Excel.Interop.tlb /codebase

@REM - below is for execution from 64 bit processes

%SYSTEMROOT%\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe bin\Release\Neuron.NetX.Excel.Interop.dll /u
%SYSTEMROOT%\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe bin\Release\Neuron.NetX.Excel.Interop.dll /tlb:Neuron.NetX.Excel.Interop.tlb /codebase
