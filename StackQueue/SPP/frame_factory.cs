/*
 * =====================================================================================
 *
 *       Filename:  frame_factory.h
 *
 *    Description:  FrameFactory class 
 *
 *        Version:  1.0
 *        Created:  14.12.2013 15:14:01
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
	public class FrameFactory
	{
			public FrameFactory()
			{
			}
			public Frame create(int nr)
			{
				return new Frame(nr);
			}
	}


}