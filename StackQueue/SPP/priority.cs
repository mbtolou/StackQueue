using System.Collections.Generic;


namespace StackQueue
{
	public class Priority : Algorithm
	{
			public Priority() : this(200)
			{
			}
	//C++ TO C# CONVERTER NOTE: Overloaded method(s) are created above to convert the following method having default parameters:
	//ORIGINAL LINE: base();
			public Priority(int at) : base()
			{
				this.ageing_time = at;
                System.IO.File.AppendAllText(
                    "proc_simulation.txt",
                 "*** Priority with ageing algorithm ***\n" +
                 "*Processes:\n" +
                "number\tentry time\tremaining time\tpriority\n"
                );
			}
			public override void run()
			{
                string out_file = "proc_simulation.txt";
				pr_list.Sort();
                List<Process>.Enumerator iter;
                Process current = null;
                Process next = null;
                System.IO.File.AppendAllText("proc_simulation.txt", "\n*Options:\n" + "ageing time: " + ageing_time + "\n");
                System.IO.File.AppendAllText("proc_simulation.txt", "\n*Results:\n");
                Dictionary<int, int>[] beginning = new Dictionary<int, int>[number];
				int completed = 0;
				bool is_next;
				bool is_current;
				int[] last_time = new int[number];
				bool interrupt; // 1 when it's time to aging process
				int beginning_time;
				for (iter = pr_list.GetEnumerator(); iter.MoveNext();)
					last_time[(iter.Current).getNumber() - 1] = (iter.Current).getEntryTime();
				while (completed < number)
				{
					interrupt = false;
					is_current = false;
					beginning_time = 0;
					for (iter = pr_list.GetEnumerator(); iter.MoveNext();)
						if (is_current == false && (iter.Current).getCompleted() == false)
						{
							is_current = true;
							current = iter.Current;
						}
					if (current.getEntryTime() <= time)
					{
						is_next = false;
						for (iter = pr_list.GetEnumerator(); iter.MoveNext();)
						{
							if ((iter.Current).getEntryTime() <= time && (iter.Current).getPriority() < current.getPriority() && (iter.Current).getCompleted() == false)
								current = iter.Current;
							else if ((iter.Current).getEntryTime() > time && (iter.Current).getPriority() < current.getPriority() && is_next == false)
							{
										next = iter.Current;
										is_next = true;
							}
						}
						System.IO.File.AppendAllText("proc_simulation.txt", "Process " + current.getNumber() + " begins execution! Current time: " + time + "\n");
						beginning_time = time;
						if (is_next == false)
						{
							while (!current.getCompleted() && interrupt == false)
							{
								time++;
								current.decrementRemainingTime();
								for (int i = 0; i < number; i++)
								{
									if ((time - last_time[i] > 0) && ((time - last_time[i]) % ageing_time == 0) && interrupt == false)
									{
										ageing( last_time, current.getNumber(), out_file);
										interrupt = true;
										last_time[current.getNumber() - 1] = time;
									}
								}
							}
                            if (current.getCompleted() == true)
                            {
                                completed++;
                                System.IO.File.AppendAllText("proc_simulation.txt", "Process " + current.getNumber() + " is done! Current time: " + (time - 1) + "\n");
                            }
                            else
                                System.IO.File.AppendAllText("proc_simulation.txt", "Process " + current.getNumber() + " was expropriated! Current time: " + (time - 1) + "\n");
							beginning[current.getNumber() - 1][beginning_time] = time - beginning_time - 1;
						}
						else
						{
							while (next.getEntryTime() - time > 0 && interrupt == false)
							{
								time++;
								current.decrementRemainingTime();
								for (int i = 0; i < number; i++)
								{
									if ((time - last_time[i] > 0) && ((time - last_time[i]) % ageing_time == 0) && interrupt == false)
									{
										ageing( last_time, current.getNumber(), out_file);
										interrupt = true;
										last_time[current.getNumber() - 1] = time;
									}
								}
							}
                            System.IO.File.AppendAllText("proc_simulation.txt", "Process " + current.getNumber() + " was expropriated! Current time: " + (time - 1) + "\n");
							beginning[current.getNumber() - 1][beginning_time] = time - beginning_time - 1;
						}
					}
					else
						time++;
				}
                System.IO.File.AppendAllText("proc_simulation.txt", "Average waiting time: ");
				double average = 0;
				Dictionary< int, int >.Enumerator map_iter;
				for (int i = 0; i != number; ++i)
				{
					for (map_iter = beginning[i].GetEnumerator(); map_iter.MoveNext();)
					{
						average -= map_iter.Current.Value;
					}
	//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
					average += map_iter.Current.Key + map_iter.Current.Value;
				}
				average /= number;
				System.IO.File.AppendAllText("proc_simulation.txt",  average + "\n\n");
			}
			public new void Dispose()
			{
				reset();
				base.Dispose();
			}

			protected int ageing_time;
			protected void ageing(int[] last_time, int number, string out_file)
			{
				for (List<Process>.Enumerator iter = pr_list.GetEnumerator(); iter.MoveNext();)
				{
					if ((time - (iter.Current).getEntryTime() > 0) && ((time - last_time[(iter.Current).getNumber() - 1]) % ageing_time == 0) && (iter.Current).getNumber() != number && (iter.Current).getCompleted()==false)
					{
						(iter.Current).incrementPriority();
						last_time[(iter.Current).getNumber() - 1] = time;
                        System.IO.File.AppendAllText(out_file, "Increased the priority of process number " + (iter.Current).getNumber() + " to " + (iter.Current).getPriority() + ". Current time: " + (time - 1) + "\n");
					}
				}
			}
	}




}