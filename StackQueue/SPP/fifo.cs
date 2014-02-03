using System.Collections.Generic;

/*
 * =====================================================================================
 *
 *       Filename:  fifo.cpp
 *
 *    Description:  FIFO methods definitions
 *
 *        Version:  1.0
 *        Created:  14.12.2013 15:46:08
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
 *       Filename:  fifo.h
 *
 *    Description:  FIFO class header
 *
 *        Version:  1.0
 *        Created:  14.12.2013 15:43:59
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
	public class FIFO : ReplacementAlgorithm
	{
			public FIFO(int frames, List< int > access) : base(frames, ref access)
			{
                System.IO.File.AppendAllText
               (
               "rep_simulation.txt",
                 "*** First in, first out algorithm ***\n"+
                 "\n*Accesses to pages:\n"+
                 "time\tnumber\n"
                 );
				int i = 0;
				for (List<int>.Enumerator iter = access_list.GetEnumerator(); iter.MoveNext(); iter.MoveNext(), ++i)
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
						if (f_iter.Current == frame_list[frame_list.Count-1])
							f_iter = frame_list.GetEnumerator();
						f_iter.Current.insertPage(a_iter.Current, time);
                        System.IO.File.AppendAllText("rep_simulation.txt", time + '\t' + f_iter.Current.getNumber() + "\t" + f_iter.Current.getPage() + "\t\tI\n");
						++fault_rate;
						f_iter.MoveNext();
					}
					else
					{
						for (List<Frame>.Enumerator access_iter = frame_list.GetEnumerator(); access_iter.MoveNext();)
							if ((access_iter.Current).getPage() == a_iter.Current)
							{
								access_iter.Current.access(time);
                                System.IO.File.AppendAllText("rep_simulation.txt", time + '\t' + access_iter.Current.getNumber() + '\t' + access_iter.Current.getPage() + "\t\tA\n");
							}
					}
					time++;
				}
                System.IO.File.AppendAllText("rep_simulation.txt", "Page-fault rate: " + fault_rate + "/" + access_list.Count + "\n\n");
			}
			public new void Dispose()
			{
				reset();
				base.Dispose();
			}
	}




}