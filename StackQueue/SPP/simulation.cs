using System;
using System.Collections.Generic;

/*
 * =====================================================================================
 *
 *       Filename:  simulation.cpp
 *
 *    Description:  Simulation methods definitions
 *
 *        Version:  1.0
 *        Created:  06.12.2013 10:02:08
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
 *       Filename:  simulation.h
 *
 *    Description:  Simulation class header
 *
 *        Version:  1.0
 *        Created:  06.12.2013 09:53:10
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
	public class Simulation
	{
			public Simulation()
			{
			}
			public void Menu()
			{
				string choice_test;
                int choice = 0;
				bool wrong = true;
				while (choice != 3)
				{
					Console.Clear();
					do
					{
						Console.Write("What do you want to do?\n");
						Console.Write("\t[1] simulate a scheduling algorithm\n");
						Console.Write("\t[2] simulate a page replacement algorithm\n");
						Console.Write("\t[3] exit\n");
						Console.Write(">> ");
                        choice_test = Console.ReadLine();
						if (is_digit(choice_test))
						{
							choice=Convert.ToInt32(choice_test) ;
							if (choice > 0 && choice <= 3)
								wrong = false;
							else
							{
								Console.Write("There is no option ");
								Console.Write(choice);
								Console.Write("\n\n");
							}
						}
						else
						{
							Console.Write("It's not a digit!\nPlease, try again.\n\n");
						}
					}
					while (wrong);
					if (choice != 3)
					{
						switch (choice)
						{
							case 1:
                                Console.Clear();
								schedulingMenu();
								break;
							case 2:
                                Console.Clear();
								replacementMenu();
								break;
						}
					}
				}
			}

			protected bool is_digit(string str)
			{
                int dig = 0;
                return int.TryParse(str, out dig);
			}

			protected void schedulingMenu()
			{
                System.IO.File.AppendAllText("proc_simulation.txt",
                "***** Process scheduling simulation *****\n\n");
                bool randomly = false;
				int how_many = howManyProcesses();
				int which = whichSchedulingAlgorithm();
				bool same_time = sameTimeEnter();
				if (same_time == false)
					randomly = enterRandomly();
				Console.Write(randomly);
				schedulingCreator(how_many, which, same_time, randomly);
			}
			protected int howManyProcesses()
			{
				string how_many_test;
                int how_many = 0;
				bool wrong = true;
				do
				{
					Console.Write("How many processes will participate in the simulation?\n");
					Console.Write(">> ");
                    how_many_test = Console.ReadLine();
					if (is_digit(how_many_test))
					{
                        how_many=Convert.ToInt32(how_many_test) ;
						wrong = false;
					}
					else
					{
						Console.Write("It's not a digit!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return how_many;
			}
			protected bool sameTimeEnter()
			{
				string same_time_test;
                bool same_time = false;
				bool wrong = true; // if same_time_test isn't 'y' or 'n'
				do
				{
					Console.Write("Do you want all the processes will entered in the same time? [y/n]\n");
					Console.Write(">> ");
                    same_time_test = Console.ReadLine();
					if (same_time_test == "y" || same_time_test == "Y")
					{
						same_time = true;
						wrong = false;
					}
					else if (same_time_test == "n" || same_time_test == "N")
					{
						same_time = false;
						wrong = false;
					}
					else
					{
						Console.Write("Wrong letter!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return same_time;
			}
			protected int whichSchedulingAlgorithm()
			{
				bool wrong = true;
				string which_test;
				int which=0;
				do
				{
					Console.Write("Which algorithm do you want to use?\n");
					Console.Write("\t[1] FCFS\n");
					Console.Write("\t[2] SJF\n");
					Console.Write("\t[3] RR\n");
					Console.Write("\t[4] Priority with ageing\n");
					Console.Write(">> ");
                    which_test = Console.ReadLine();
					if (is_digit(which_test))
					{
                        which =Convert.ToInt32(which_test) ;
						if (which <= 4 && which > 0)
							wrong = false;
						else
						{
							Console.Write("There is no option ");
							Console.Write(which);
							Console.Write("\n\n");
						}
					}
					else
					{
						Console.Write("It's not a digit!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return which;
			}
			protected bool enterRandomly()
			{
				string randomly_test;
                bool randomly = false;
				bool wrong = true; // if randomly_test isn't 'y' or 'n'
				do
				{
					Console.Write("Select process entry time randomly? [y/n]\n");
					Console.Write(">> ");
                    randomly_test = Console.ReadLine();
					if (randomly_test == "y" || randomly_test == "Y")
					{
						randomly = true;
						wrong = false;
					}
					else if (randomly_test == "n" || randomly_test == "N")
					{
						randomly = false;
						wrong = false;
					}
					else
					{
						Console.Write("Wrong letter!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return randomly;
			}
			protected void schedulingCreator(int how_many, int which, bool same_time, bool randomly)
			{
				bool exit = false;
				Process[] proc_array = new Process[how_many];
				if (same_time == false && randomly == false)
					manuallySetEntryTime(proc_array, how_many);
				else if (same_time == false)
					randomEntryTime(proc_array, how_many);
				else
					sameEntryTime(proc_array, how_many);
				do
				{
					Algorithm algor = schedulingAlgorithmsOptions(which);
					for (int i = 0; i < how_many; i++)
						algor.addProcess(proc_array[i]);
					algor.run();
					Console.Write("*** Done ***\n\n");
					if (algor != null)
						algor.Dispose();
					string repeat_test;
                    bool repeat = false;
					bool wrong = true;
					do
					{
						Console.Write("Do you want to repeat simulation on the same processes using a different algorithm? [y/n]\n");
						Console.Write(">> ");
                         repeat_test = Console.ReadLine();
						if (repeat_test == "y" || repeat_test == "Y")
						{
							repeat = true;
							wrong = false;
						}
						else if (repeat_test == "n" || repeat_test == "N")
						{
							repeat = false;
							wrong = false;
						}
						else
						{
							Console.Write("Wrong letter!\nPlease, try again.\n\n");
						}
					}
					while (wrong);
					if (repeat)
						which = whichSchedulingAlgorithm();
					else
						exit = true;
				}
				while (exit == false);
				Console.Write("\n\n");

			}
			protected void manuallySetEntryTime(Process[]array, int how_many)
			{
				string entry_time_test;
                int entry_time = 0;
				bool wrong;
				ProcessFactory factory = new ProcessFactory();
				for (int i = 0; i < how_many; i++)
				{
					wrong = true;
					do
					{
						Console.Write("Enter the start time of process number ");
						Console.Write(i + 1);
						Console.Write("\n");
						Console.Write(">> ");
                         entry_time_test = Console.ReadLine();
						if (is_digit(entry_time_test))
						{
                            entry_time=Convert.ToInt32(entry_time_test);
							wrong = false;
						}
						else
						{
							Console.Write("It's not a digit!\nPlease, try again.\n\n");
						}
					}
					while (wrong);
					Console.Write("\n\n");
					array[i] = factory.create(entry_time);
				}
			}
			protected void randomEntryTime(Process[]array, int how_many)
			{
				ProcessFactory factory = new ProcessFactory();
				for (int i = 0; i < how_many; i++)
					array[i] = factory.create();
			}
			protected void sameEntryTime(Process[]array, int how_many)
			{
				ProcessFactory factory = new ProcessFactory();
				for (int i = 0; i < how_many; i++)
					array[i] = factory.create(0);
			}
			protected Algorithm schedulingAlgorithmsOptions(int which)
			{
                Algorithm pointer = null;
				switch (which)
				{
					case 1:
						pointer = new FCFS();
						break;
					case 2:
						pointer = new SJF();
						break;
					case 3:
						pointer = RROptions();
						break;
					case 4:
						pointer = priorityOptions();
						break;
				}
				return pointer;
			}
			protected RoundRobin RROptions()
			{
				string time_slice_change_test;
                bool time_slice_change = false;
				bool wrong = true;
				do
				{
					Console.Write("Do you want to change the standard length of time slice (80)? [y/n]\n");
					Console.Write(">> ");
                     time_slice_change_test = Console.ReadLine();
					if (time_slice_change_test == "y" || time_slice_change_test == "Y")
					{
						time_slice_change = true;
						wrong = false;
					}
					else if (time_slice_change_test == "n" || time_slice_change_test == "N")
					{
						time_slice_change = false;
						wrong = false;
					}
					else
					{
						Console.Write("Wrong letter!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				if (time_slice_change)
				{
					string time_slice_test;
                    int time_slice = 0;
					do
					{
						Console.Write("Type your new length of time slice\n");
						Console.Write(">> ");
                        time_slice_test = Console.ReadLine();
						if (is_digit(time_slice_test))
						{
                            time_slice=Convert.ToInt32(time_slice_test);
							wrong = false;
						}
						else
						{
							Console.Write("It's not a digit!\nPlease, try again.\n\n");
						}
					}
					while (wrong);
					Console.Write("\n\n");
					return new RoundRobin(time_slice);
				}
				else
					return new RoundRobin();
			}
			protected Priority priorityOptions()
			{
				string ageing_time_change_test;
				bool ageing_time_change=false;
				bool wrong = true;
				do
				{
					Console.Write("Do you want to change the standard ageing time slice (200)? [y/n]\n");
					Console.Write(">> ");
                   ageing_time_change_test = Console.ReadLine();
					if (ageing_time_change_test == "y" || ageing_time_change_test == "Y")
					{
						ageing_time_change = true;
						wrong = false;
					}
					else if (ageing_time_change_test == "n" || ageing_time_change_test == "N")
					{
						ageing_time_change = false;
						wrong = false;
					}
					else
					{
						Console.Write("Wrong letter!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				if (ageing_time_change)
				{
					string ageing_time_test;
					int ageing_time=0;
					do
					{
						Console.Write("Type your new length of ageing time slice\n");
						Console.Write(">> ");
                        ageing_time_test = Console.ReadLine();
						if (is_digit(ageing_time_test))
						{
							ageing_time=Convert.ToInt32(ageing_time_test)  ;
							wrong = false;
						}
						else
						{
							Console.Write("It's not a digit!\nPlease, try again.\n\n");
						}
					}
					while (wrong);
					Console.Write("\n\n");
					return new Priority(ageing_time);
				}
				else
					return new Priority();
			}
			protected void replacementMenu()
			{
                System.IO.File.AppendAllText("rep_simulation.txt", 
				"***** Page replacement simulation *****\n\n");
				int how_many_frames = howManyFrames();
				int accesses = howManyAccesses();
				int max_page_number = maxPageNumber();
				bool random = randomAccesses();
				int which = whichReplacementAlgorithm();
				replacementCreator(how_many_frames, accesses, max_page_number, random, which);
			}
			protected int howManyFrames()
			{
				string how_many_test;
                int how_many = 0;
				bool wrong = true;
				do
				{
					Console.Write("How many frames will be available in the simulation?\n");
					Console.Write(">> ");
                     how_many_test = Console.ReadLine();
					if (is_digit(how_many_test))
					{
						how_many=Convert.ToInt32(how_many_test) ;
						wrong = false;
					}
					else
					{
						Console.Write("It's not a digit!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return how_many;
			}
			protected int howManyAccesses()
			{
				string how_many_test;
                int how_many = 0;
				bool wrong = true;
				do
				{
					Console.Write("How many memory accesses will be in the simulation?\n");
					Console.Write(">> ");
                    how_many_test = Console.ReadLine();
					if (is_digit(how_many_test))
					{
						how_many=Convert.ToInt32(how_many_test) ;
						wrong = false;
					}
					else
					{
						Console.Write("It's not a digit!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return how_many;
			}
			protected int maxPageNumber()
			{
				bool wrong = true;
				string number_test;
                int number = 0;
				do
				{
					Console.Write("Enter maximal page number.\n");
					Console.Write(">> ");
                     number_test = Console.ReadLine();
					if (is_digit(number_test))
					{
						number=Convert.ToInt32(number_test) ;
						if (number > 1)
							wrong = false;
						else
						{
							Console.Write("Maximal number of pages can't be less than 1\n\n");
						}
					}
					else
					{
						Console.Write("It's not a digit!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return number;
			}
			protected bool randomAccesses()
			{
				string random_test;
                bool random = false;
				bool wrong = true; // if randomly_test isn't 'y' or 'n'
				do
				{
					Console.Write("Select randomly the order of access to memory pages? [y/n]\n");
					Console.Write(">> ");
                    random_test = Console.ReadLine();
					if (random_test == "y" || random_test == "Y")
					{
						random = true;
						wrong = false;
					}
					else if (random_test == "n" || random_test == "N")
					{
						random = false;
						wrong = false;
					}
					else
					{
						Console.Write("Wrong letter!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return random;
			}
			protected int whichReplacementAlgorithm()
			{
				bool wrong = true;
				string which_test;
                int which = 0;
				do
				{
					Console.Write("Which algorithm do you want to use?\n");
					Console.Write("\t[1] FIFO\n");
					Console.Write("\t[2] OPT\n");
					Console.Write("\t[3] LRU\n");
					Console.Write(">> ");
                    which_test = Console.ReadLine();
					if (is_digit(which_test))
					{
						which=Convert.ToInt32(which_test) ;
						if (which <= 3 && which > 0)
							wrong = false;
						else
						{
							Console.Write("There is no option ");
							Console.Write(which);
							Console.Write("\n\n");
						}
					}
					else
					{
						Console.Write("It's not a digit!\nPlease, try again.\n\n");
					}
				}
				while (wrong);
				Console.Write("\n\n");
				return which;
			}
			protected void replacementCreator(int frames, int accesses, int max_page_number, bool random, int which)
			{
				bool exit = false;
				List< int > access_list = new List< int >();
				if (random == false)
					access_list = new List< int >(manuallySetAccesses(accesses, max_page_number));
				else
					access_list = new List< int >(randomAccesses(accesses, max_page_number));
				do
				{
					ReplacementAlgorithm algor = replacementAlgorithmCreator(which, frames,  access_list);
					algor.run();
					Console.Write("*** Done ***\n\n");
					if (algor != null)
						algor.Dispose();
					string repeat_test;
                    bool repeat = false;
					bool wrong = true;
					do
					{
						Console.Write("Do you want to repeat simulation on the same queue of accesses to memory pages using a different algorithm? [y/n]\n");
						Console.Write(">> ");
                        repeat_test = Console.ReadLine();
						if (repeat_test == "y" || repeat_test == "Y")
						{
							repeat = true;
							wrong = false;
						}
						else if (repeat_test == "n" || repeat_test == "N")
						{
							repeat = false;
							wrong = false;
						}
						else
						{
							Console.Write("Wrong letter!\nPlease, try again.\n\n");
						}
					}
					while (wrong);
					if (repeat)
						which = whichReplacementAlgorithm();
					else
						exit = true;
				}
				while (exit == false);
				Console.Write("\n\n");
			}
			protected List< int > manuallySetAccesses(int how_many, int max_page_number)
			{
				List< int > access_list = new List< int >();
				string page_test;
				int page = -1;
				bool wrong;
				for (int i = 0; i < how_many; i++)
				{
					wrong = true;
					do
					{
						Console.Write("Time: ");
						Console.Write(i + 1);
						Console.Write(". Enter page number. <1;");
						Console.Write(max_page_number);
						Console.Write(">\n");
						Console.Write(">> ");
                        page_test = Console.ReadLine();
						if (is_digit(page_test))
						{
                            page=Convert.ToInt32(page_test) ;
							if (page >= 1 && page <= max_page_number)
							{
								wrong = false;
								access_list.Add(page);
							}
							else
							{
								Console.Write("Wrong page number!\nPlease, try again.\n\n");
							}
						}
						else
						{
							Console.Write("It's not a digit!\nPlease, try again.\n\n");
						}
					}
					while (wrong);
					Console.Write("\n\n");
				}
				Console.Write("\n\n");
				return access_list;
			}
			protected List< int > randomAccesses(int how_many, int max_page_number)
			{
				List< int > access_list = new List< int >();
				int page = 0;
				for (int i = 0; i < how_many; ++i)
				{
					page = RandomNumbers.NextNumber() % max_page_number + 1;
					access_list.Add(page);
				}
				return access_list;
			}
			protected ReplacementAlgorithm replacementAlgorithmCreator(int which, int frames, List< int > access_list)
			{
                ReplacementAlgorithm pointer = null;
				switch (which)
				{
					case 1:
						pointer = new FIFO(frames,  access_list);
						break;
					case 2:
						pointer = new OPT(frames,  access_list);
						break;
					case 3:
						pointer = new LRU(frames,  access_list);
						break;
				}
				return pointer;
			}
	}




}