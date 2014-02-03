using System.Collections.Generic;

namespace StackQueue
{
	public abstract class ReplacementAlgorithm
	{
			public ReplacementAlgorithm(int frames,ref List< int > a_list)
			{
				this.time = 0;
				FrameFactory factory = new FrameFactory();
				for (int i = 0; i < frames; i++)
				{
					frame_list.Add(factory.create(i + 1));
				}
				access_list = new List< int >(a_list);
			}
			public abstract void run();
			public virtual void Dispose()
			{
				reset();
			}

			protected void reset()
			{
				access_list.Clear();
				frame_list.Clear();
				time = 0;
			}
			protected bool found(int page)
			{
				List< Frame  >.Enumerator iter;
				for (iter = frame_list.GetEnumerator(); iter.MoveNext();)
				{
					if ((iter.Current).getPage() == page)
						return true;
				}
				return false;
			}
			protected List< int > access_list = new List< int >();
			protected List< Frame  > frame_list = new List< Frame  >();
			protected int time;
	}




}