/*
 * =====================================================================================
 *
 *       Filename:  fcfs.cpp
 *
 *    Description:  FCFS methods definitions 
 *
 *        Version:  1.0
 *        Created:  01.12.2013 14:52:56
 *       Revision:  none
 *       Compiler:  gcc
 *
 *         Author:  Bartłomiej Małecki, 
 *   Organization:  
 *
 * =====================================================================================
 */
/*
 * =====================================================================================
 *
 *       Filename:  fcfs.h
 *
 *    Description:  FCFS class header
 *
 *        Version:  1.0
 *        Created:  01.12.2013 14:50:01
 *       Revision:  none
 *       Compiler:  gcc
 *
 *         Author:  Bartłomiej Małecki, 
 *   Organization:  
 *
 * =====================================================================================
 */

using System.Collections.Generic;
namespace StackQueue
{
	public class FCFS : Algorithm
	{
        public FCFS()
            : base()
        {
            System.IO.File.AppendAllText
                (
                "proc_simulation.txt",
             "*** First-come, first-served algorithm ***\n" +
             "*Processes:\n" +
             "number\tentry time\tremaining time\tpriority\n"
             );
        }
			public override void run()
			{
				pr_list.Sort();
				List< Process  >.Enumerator iter;
				int[] beginning = new int[number];
				for (int i = 0; i != number; i++)
					beginning[i] = -1;

                System.IO.File.AppendAllText("proc_simulation.txt", "\n*Results:\n");

				for (iter = pr_list.GetEnumerator(); iter.MoveNext();)
				{
					while (!(iter.Current).getCompleted())
					{
						if (time < (iter.Current).getEntryTime())
							time++;
						else
						{
							if (beginning[(iter.Current).getNumber() - 1] < 0)
							{
                                System.IO.File.AppendAllText("proc_simulation.txt", "Process " + (iter.Current).getNumber() + " begins execution! Current time: " + time + "\n");
                                //out_file + "Process " + (iter.Current).getNumber() + " begins execution! Current time: " + time + "\n";
								beginning[(iter.Current).getNumber() - 1] = time - (iter.Current).getEntryTime();
							}
							(iter.Current).decrementRemainingTime();
							if ((iter.Current).getCompleted())
                                System.IO.File.AppendAllText("proc_simulation.txt", "Process " + (iter.Current).getNumber() + " is done! Current time: " + time + "\n");
                                //out_file + "Process " + (iter.Current).getNumber() + " is done! Current time: " + time + "\n";
							time++;
						}
					}
				}
				double average = 0;
				for (int i = 0; i != number; i++)
					average += beginning[i];
				average /= number;

                System.IO.File.AppendAllText("proc_simulation.txt", "Average waiting time: " + average+"\n");

			}
			public new void Dispose()
			{
				reset();
				base.Dispose();
			}
	}




}