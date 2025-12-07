double Funcx(double[] dParams)
        {
            return dParams[0] * dParams[0];
        }

        double Limits(double[] dParams)
        {
            if (dParams[0] < -1 || dParams[0] > 1 ) return -1;
            return 1;
        }
		public void Execute()
        {
            Dynamo.Console("GradientSearch started!");
           MathPanelExt.GradientSearch grad = new MathPanelExt.GradientSearch();
            grad.m_numParam = 1;

            grad.m_funcExternal = Funcx;
            grad.m_funcOverlimits = Limits;

            double dd = grad.doGradient(1, 10);
            Dynamo.Console("dd=" + dd + ", x=" + grad.m_dParams[0]);
        }  
Execute();
