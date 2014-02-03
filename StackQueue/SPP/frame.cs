/*
 * =====================================================================================
 *
 *       Filename:  frame.cpp
 *
 *    Description:  frame methods definitions
 *
 *        Version:  1.0
 *        Created:  14.12.2013 14:35:21
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
 *       Filename:  frame.h
 *
 *    Description:  Frame class header
 *
 *        Version:  1.0
 *        Created:  14.12.2013 14:20:00
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
	public class Frame
	{
			public Frame(int nr)
			{
				this.page_number = 0;
				this.last_access_time = -1;
				this.number = nr;
			}
			public int getPage()
			{
				return page_number;
			}
			public void insertPage(int p_number, int time)
			{
				page_number = p_number;
				last_access_time = time;
			}
			public int getLastAccessTime()
			{
				return last_access_time;
			}
			public void access(int a_time)
			{
				last_access_time = a_time;
			}
			public int getNumber()
			{
				return number;
			}

			protected int page_number; //number of currently stored page
			protected int last_access_time;
			protected int number;
	}




}