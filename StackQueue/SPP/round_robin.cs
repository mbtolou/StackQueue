using System.Collections.Generic;

/*
 * =====================================================================================
 *
 *       Filename:  round_robin.cpp
 *
 *    Description:  RoundRobin methods definitions 
 *
 *        Version:  1.0
 *        Created:  01.12.2013 21:34:56
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
 *       Filename:  round_robin.h
 *
 *    Description:  RoundRobin class header 
 *
 *        Version:  1.0
 *        Created:  01.12.2013 19:15:04
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
	public class RoundRobin : Algorithm
	{
			public RoundRobin() : this(80)
			{
			}
	//C++ TO C# CONVERTER NOTE: Overloaded method(s) are created above to convert the following method having default parameters:
	//ORIGINAL LINE: base();
			public RoundRobin(int ts) : base()
			{
				this.time_slice_length = ts;
                System.IO.File.AppendAllText("proc_simulation.txt",
                    "*** Round Robin algorithm ***\n" +
                 "*Processes:\n" +
                 "number\tentry time\tremaining time\tpriority\n");
			}
			public override void run()
			{
				pr_list.Sort();
				List< Process  >.Enumerator iter;
				System.IO.File.AppendAllText("proc_simulation.txt", "\n*Options:\n" + "length of time slice: " + time_slice_length + "\n");
				System.IO.File.AppendAllText("proc_simulation.txt", "\n*Results:\n");
				int completed = 0;
				int slice_timer;
				List< int >[] beginning = new List<int>[number];
                for (int i = 0; i < number; i++)
                    beginning[i] = new List<int>();

				while (completed < number)
				{
					for (iter = pr_list.GetEnumerator(); iter.MoveNext();)
					{
						slice_timer = 0;
						int start = -1;
						while (!(iter.Current).getCompleted() && slice_timer < time_slice_length)
						{
							bool last_process = false;
							if (time < (iter.Current).getEntryTime())
								time++;
							else
							{
								if (start < 0)
								{
                                    //if (beginning[iter.Current.getNumber() - 1] + time_slice_length != time - (iter.Current).getEntryTime() && last_process != true)
                                    //{
                                    //    start = time - (iter.Current).getEntryTime();
                                    //    beginning[(iter.Current).getNumber() - 1].Add(start);
                                    //}
                                    //else
                                    //    last_process = true;
									System.IO.File.AppendAllText("proc_simulation.txt", "Process " + (iter.Current).getNumber() + " begins execution! Current time: " + time + "\n");
								}
								(iter.Current).decrementRemainingTime();
								slice_timer++;
								if ((iter.Current).getCompleted())
									System.IO.File.AppendAllText("proc_simulation.txt",  "Process " + (iter.Current).getNumber() + " is done! Current time: " + time + "\n");
								else if (slice_timer == time_slice_length)
									System.IO.File.AppendAllText("proc_simulation.txt", "Process " + (iter.Current).getNumber() + " was expropriated! Current time: " + time + "\n");
								time++;
								if ((iter.Current).getCompleted())
								{
									completed++;
								}
							}
						}
					}
				}
				System.IO.File.AppendAllText("proc_simulation.txt",  "Average waiting time: ");
				double average = 0;
				for (int i = 0; i != number; i++)
					average += beginning[i][beginning[i].Count-1] - (beginning[i].Count - 1) * time_slice_length;
				average /= number;
				System.IO.File.AppendAllText("proc_simulation.txt",  average + "\n\n");
			}
			public new void Dispose()
			{
				reset();
				base.Dispose();
			}

			protected int time_slice_length;
	}




}