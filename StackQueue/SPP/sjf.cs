using System.Collections.Generic;

/*
 * =====================================================================================
 *
 *       Filename:  sjf.cpp
 *
 *    Description:  SJF methods definitions 
 *
 *        Version:  1.0
 *        Created:  03.12.2013 16:05:33
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
 *       Filename:  sjf.h
 *
 *    Description:  SJF class header
 *
 *        Version:  1.0
 *        Created:  02.12.2013 17:02:11
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
	public class SJF : Algorithm
	{
			public SJF() : base()
			{
                System.IO.File.AppendAllText("proc_simulation.txt",
                "*** Shortest jobs first algorithm ***\n" +
                 "*Processes:\n" +
                "number\tentry time\tremaining time\tpriority\n");
			}
			public override void run()
			{
				//pr_list.Sort();
				List< Process  >.Enumerator iter;
                Process current = null;
                Process next = null;
                System.IO.File.AppendAllText("proc_simulation.txt", "\n*Results:\n");
                Dictionary<int, int>[] beginning = new Dictionary<int, int>[number] ;
                for (int i = 0; i < number; i++)
                    beginning[i] = new Dictionary<int, int>();
				int completed = 0;
				bool is_next = false;
				bool is_current;
				while (completed < number)
				{
					is_current = false;
					for (iter = pr_list.GetEnumerator(); iter.MoveNext();)
						if (is_current == false && iter.Current.getCompleted() == false)
						{
							is_current = true;
							current = iter.Current;
						}
					if (current.getEntryTime() <= time)
					{
						is_next = false;
						for (iter = pr_list.GetEnumerator(); iter.MoveNext();)
						{
							if ((iter.Current).getEntryTime() <= time && (iter.Current).getRemainingTime() < current.getRemainingTime() && (iter.Current).getCompleted() == false)
								current = iter.Current;
							else if ((iter.Current).getEntryTime() > time && (iter.Current).getRemainingTime() < (current.getRemainingTime() - ((iter.Current).getEntryTime() - time)) && is_next == false)
							{
										next = iter.Current;
										is_next = true;
							}
						}
                        System.IO.File.AppendAllText("proc_simulation.txt", "Process " + current.getNumber() + " begins execution! Current time: " + time + "\n");
						if (is_next == false)
							beginning[current.getNumber() - 1][time] = current.getRemainingTime();
						else
						{
							int remaining_time = next.getEntryTime() - time;
							beginning[current.getNumber() - 1][time] = remaining_time;
						}
						if (is_next == false)
						{
							while (!current.getCompleted())
							{
								current.decrementRemainingTime();
								time++;
							}
							completed++;
                            System.IO.File.AppendAllText("proc_simulation.txt", "Process " + current.getNumber() + " is done! Current time: " + (time - 1) + "\n");
						}
						else
						{
							while (next.getEntryTime() - time > 0)
							{
								current.decrementRemainingTime();
								time++;
							}
                            System.IO.File.AppendAllText("proc_simulation.txt", "Process " + current.getNumber() + " was expropriated! Current time: " + (time - 1) + "\n");
						}
					}
					else
						time++;
				}
				System.IO.File.AppendAllText("proc_simulation.txt", "Average waiting time: ");
				double average = 0;
				Dictionary< int, int >.Enumerator map_iter;
                KeyValuePair<int, int> last = new KeyValuePair<int,int>();
				for (int i = 0; i != number; ++i)
				{
					for (map_iter = beginning[i].GetEnumerator(); map_iter.MoveNext();)
					{
						average -= map_iter.Current.Value;
                        last = map_iter.Current;
					}
	//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
                    average += last.Key + last.Value;
				}
				average /= number;
				System.IO.File.AppendAllText("proc_simulation.txt",average + "\n\n");
			}
			public new void Dispose()
			{
				reset();
				base.Dispose();
			}
	}




}