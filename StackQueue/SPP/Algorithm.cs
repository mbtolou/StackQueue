using System.Collections.Generic;

/*
 * =====================================================================================
 *
 *       Filename:  algorithm.h
 *
 *    Description:  algorithm class header 
 *
 *        Version:  1.0
 *        Created:  01.12.2013 11:19:29
 *       Revision:  none
 *       Compiler:  gcc
 *
 *         Author:  Bartłomiej Małecki, 
 *   Organization:  
 *
 * =====================================================================================
 */


namespace StackQueue
{
	public abstract class Algorithm
	{
		public Algorithm()
		{
			this.number = 0;
			this.time = 0;
		}
		public void addProcess(Process proc)
		{
			number++;
			if (proc.getCompleted() == true)
				proc.setUncompleted();
			if (proc.getRemainingTime() == 0)
				proc.resetRemainingTime();
			proc.resetPriority();
			proc.setNumber(number);
			pr_list.Add(proc);
            //ofstream out_file = new ofstream();
            //out_file.open("proc_simulation.txt", ios_base.app);
            System.IO.File.AppendAllText("proc_simulation.txt", proc.getNumber() + '\t' + proc.getEntryTime() + "\t\t" + proc.getRemainingTime() + "\t\t" + proc.getPriority() + "\n");
            //out_file.close();
		}
			public abstract void run();
		public int getNumber()
		{
			return number;
		}
		public void Dispose()
		{
			reset();
		}

		public void reset()
		{
			time = 0;
			number = 0;
			pr_list.Clear();
		}
			protected List< Process  > pr_list = new List< Process  >(); //list of processes
			protected int number;
			protected int time;
	}


}