using System.Collections.Generic;

/*
 * =====================================================================================
 *
 *       Filename:  opt.cpp
 *
 *    Description:  OPT methods definitions
 *
 *        Version:  1.0
 *        Created:  15.12.2013 00:03:49
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
 *       Filename:  opt.h
 *
 *    Description:  OPT class header
 *
 *        Version:  1.0
 *        Created:  15.12.2013 00:00:04
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
	public class OPT : ReplacementAlgorithm
	{
			public OPT(int frames, List< int > access) : base(frames, ref access)
			{
                System.IO.File.AppendAllText("rep_simulation.txt",
                "*** Opltimal algorithm ***\n" +
                "\n*Accesses to pages:\n" +
                 "time\tnumber\n");

				int i = 0;
				for (List<int>.Enumerator iter = access_list.GetEnumerator(); iter.MoveNext();iter.MoveNext(), ++i)
					System.IO.File.AppendAllText("rep_simulation.txt", i + "\t" + iter.Current + "\n");
				System.IO.File.AppendAllText("rep_simulation.txt", "\n*Frames: " + frame_list.Count + "\n");
			}
			public override void run()
			{
				int fault_rate = 0;
				List< int >.Enumerator a_iter;
				List< Frame  >.Enumerator f_iter = frame_list.GetEnumerator();

                System.IO.File.AppendAllText("rep_simulation.txt", "\n*Results:\n" + "time\tframe\tpage number\tA/I\n");
				for (a_iter = access_list.GetEnumerator(); a_iter.MoveNext();)
				{
					if (!found(a_iter.Current))
					{
						Dictionary< int, int > pages = new Dictionary< int, int >(); //page number and next access time
                        if (frame_list[frame_list.Count - 2].getPage() > 0)
						{
							for (f_iter = frame_list.GetEnumerator(); f_iter.MoveNext();)
							{
								if ((f_iter.Current).getPage() > 0)
									pages[(f_iter.Current).getPage()] = 0;
							}
							List< int >.Enumerator next_access;
							Dictionary< int, int >.Enumerator m_iter;
							for (m_iter = pages.GetEnumerator(); m_iter.MoveNext();)
							{
								int when = 0;
								bool foundd = false;
                                for (next_access = a_iter; foundd == false && next_access.Current != access_list[access_list.Count-1]; next_access.MoveNext())
								{
									++when;
									if (next_access.Current == m_iter.Current.Key)
									{
										foundd = true;
										pages[next_access.Current] = when;
									}
								}
							}
							int max_time = 0;
							int victim_frame = -1;
							for (m_iter = pages.GetEnumerator(); m_iter.MoveNext();)
							{
								if (max_time <= m_iter.Current.Value)
								{
								max_time = m_iter.Current.Value;
								victim_frame = m_iter.Current.Key;
								}
							}
							for (f_iter = frame_list.GetEnumerator(); f_iter.MoveNext();)
							{
								if ((f_iter.Current).getPage() == victim_frame)
								{
									(f_iter.Current).insertPage(a_iter.Current, time);
                                    System.IO.File.AppendAllText("proc_simulation.txt", time + '\t' + (f_iter.Current).getNumber() + "\t" + (f_iter.Current).getPage() + "\t\tI\n");
								}
							}
						}
						else
						{
							bool foundd = false;
                            for (f_iter = frame_list.GetEnumerator(); f_iter.Current != frame_list[frame_list.Count-1] && foundd == false; f_iter.MoveNext())
							{
								if ((f_iter.Current).getPage() == 0)
								{
									foundd = true;
									(f_iter.Current).insertPage(a_iter.Current, time);
                                    System.IO.File.AppendAllText("proc_simulation.txt", time + '\t' + (f_iter.Current).getNumber() + "\t" + (f_iter.Current).getPage() + "\t\tI\n");
								}
							}
						}
						++fault_rate;
					}
					else
					{
						for (f_iter = frame_list.GetEnumerator(); f_iter.MoveNext();)
							if ((f_iter.Current).getPage() == a_iter.Current)
							{
								(f_iter.Current).access(time);
								System.IO.File.AppendAllText("proc_simulation.txt", time + '\t' + (f_iter.Current).getNumber() + "\t" + (f_iter.Current).getPage() + "\t\tA\n");
							}
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