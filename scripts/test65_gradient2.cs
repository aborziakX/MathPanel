﻿using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

///assemblies to use
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
	public class Script
	{  
        double Funcx(double[] dParams)
        {
            return dParams[0] * dParams[0] - dParams[1] * dParams[1];
        }

        double Limits(double[] dParams)
        {
            if (dParams[0] < -1 || dParams[0] > 1 || dParams[1] < -1 || dParams[1] > 1 ) return -1;
            return 1;
        }
		public void Execute()
        {
            Dynamo.Console("GradientSearch started!");
           MathPanelExt.GradientSearch grad = new MathPanelExt.GradientSearch();
            grad.m_numParam = 2;

            grad.m_funcExternal = Funcx;
            grad.m_funcOverlimits = Limits;

            double dd = grad.doGradient(10, 100);
            Dynamo.Console("dd=" + dd);
for(int i = 0; i < 2; i++ ) Dynamo.Console("par[" + i + "]=" + grad.m_dParams[i]);
        }  
    }  
}