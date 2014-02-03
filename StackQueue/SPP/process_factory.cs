/*
 * =====================================================================================
 *
 *       Filename:  process_factory.h
 *
 *    Description:  ProcessFactory class
 *
 *        Version:  1.0
 *        Created:  07.12.2013 12:22:31
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
	public class ProcessFactory
	{
			public ProcessFactory()
			{
			}
			public Process create()
			{
				return new Process();
			}
			public Process create(int entry_time)
			{
				return new Process(entry_time);
			}
	}


}