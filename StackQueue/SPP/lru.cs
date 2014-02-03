using System.Collections.Generic;

/*
 * =====================================================================================
 *
 *       Filename:  lru.cpp
 *
 *    Description:  LRU methods definitions
 *
 *        Version:  1.0
 *        Created:  14.12.2013 22:10:43
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
 *       Filename:  lru.h
 *
 *    Description:  LRU class header
 *
 *        Version:  1.0
 *        Created:  14.12.2013 22:08:26
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
	public class LRU : ReplacementAlgorithm
	{
			public LRU(int frames, List< int > access) : base(frames, ref access)
			{
                System.IO.File.AppendAllText("proc_simulation.txt",
                 "*** Last recently used algorithm ***\n" +
                 "\n*Accesses to pages:\n" +
                 "time\tnumber\n");
				int i = 0;
                for (List<int>.Enumerator iter = access_list.GetEnumerator(); iter.MoveNext(); iter.MoveNext(), ++i)
                    System.IO.File.AppendAllText("proc_simulation.txt", i + "\t" + iter.Current + "\n");
				System.IO.File.AppendAllText("proc_simulation.txt", "\n*Frames: " + frame_list.Count + "\n");
			}
			public override void run()
			{
				int fault_rate = 0;
				List< int >.Enumerator a_iter;
				int frame_victim = 0;
				List< Frame  >.Enumerator f_iter = frame_list.GetEnumerator();

                System.IO.File.AppendAllText("proc_simulation.txt",  "\n*Results:\n" + "time\tframe\tpage number\tA/I\n");
				for (a_iter = access_list.GetEnumerator(); a_iter.MoveNext();)
				{
					if (!found(a_iter.Current))
					{
						int when = time;
						for (f_iter = frame_list.GetEnumerator(); f_iter.MoveNext();)
							if ((f_iter.Current).getLastAccessTime() < when)
							{
								when = (f_iter.Current).getLastAccessTime();
								frame_victim = (f_iter.Current).getNumber();
							}
						for (f_iter = frame_list.GetEnumerator(); f_iter.MoveNext();)
							if ((f_iter.Current).getNumber() == frame_victim)
							{
								(f_iter.Current).insertPage(a_iter.Current, time);
								System.IO.File.AppendAllText("proc_simulation.txt", time + '\t' + (f_iter.Current).getNumber() + "\t" + (f_iter.Current).getPage() + "\t\tI\n");
								continue;
							}
						++fault_rate;
					}
					else
						for (f_iter = frame_list.GetEnumerator(); f_iter.MoveNext();)
							if ((f_iter.Current).getPage() == a_iter.Current)
							{
	//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
								(f_iter.Current).access(time);
	//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
                                System.IO.File.AppendAllText("proc_simulation.txt", time + '\t' + (f_iter.Current).getNumber() + "\t" + (f_iter.Current).getPage() + "\t\tA\n");
								continue;
							}
					time++;
				}
                System.IO.File.AppendAllText("proc_simulation.txt", "Page-fault rate: " + fault_rate + "/" + access_list.Count + "\n\n");

			}
			public new void Dispose()
			{
				reset();
				base.Dispose();
			}
	}




}