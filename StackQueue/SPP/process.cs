/*
 * =====================================================================================
 *
 *       Filename:  process.cpp
 *
 *    Description:  process methods definitions
 *
 *        Version:  1.0
 *        Created:  30.11.2013 21:11:49
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
 *       Filename:  process.h
 *
 *    Description:  process class header
 *
 *        Version:  1.0
 *        Created:  30.11.2013 20:56:34
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
	public class Process
	{
		  public Process()
		  {
			  this.completed = false;
			  this.number = 0;
			  priority = RandomNumbers.NextNumber() % 10 + 1;
			  rem_time = RandomNumbers.NextNumber() % 100 + 1;
              bool initial = (RandomNumbers.NextNumber() % 1 == 0);
			  if (initial)
				  ent_time = 0;
			  else
				  ent_time = RandomNumbers.NextNumber() % 100 + 1;
			  const_rem_time = rem_time;
			  const_priority = priority;
		  }
		  public Process(int ent)
		  {
			  this.ent_time = ent;
			  this.completed = false;
			  this.number = 0;
			  priority = RandomNumbers.NextNumber() % 10 + 1;
			  rem_time = RandomNumbers.NextNumber() % 100 + 1;
			  const_rem_time = rem_time;
			  const_priority = priority;
		  }
		  public int getEntryTime()
		  {
			  return ent_time;
		  }
		  public int getRemainingTime()
		  {
			  return rem_time;
		  }
		  public int getPriority()
		  {
			  return priority;
		  }
		  public bool getCompleted()
		  {
			  return completed;
		  }
		  public int getNumber()
		  {
			  return number;
		  }
		  public void incrementPriority()
		  {
			  if (priority > 2)
				priority--;
		  }
		  public void setNumber(int nr)
		  {
			  number = nr;
		  }
		  public void decrementRemainingTime()
		  {
			  rem_time--;
			  if (rem_time == 0)
				  completed = true;
		  }
		  public static bool operator < (Process ImpliedObject, Process proc)
		  {
              if (ImpliedObject.ent_time < proc.getEntryTime())
				  return true;
			  else
				  return false;
		  }

          public static bool operator >(Process ImpliedObject, Process proc)
          {
              if (ImpliedObject.ent_time < proc.getEntryTime())
                  return false;
              else
                  return true;
          } 
		  public void setUncompleted()
		  {
			  completed = false;
		  }
		  public void resetRemainingTime()
		  {
			  rem_time = const_rem_time;
		  }
		  public void resetPriority()
		  {
			  priority = const_priority;
		  }

		  protected int priority;
		  protected int rem_time;
		  protected int ent_time;
		  protected bool completed;
		  protected int number;
		  protected int const_rem_time;
		  protected int const_priority;
	}




}