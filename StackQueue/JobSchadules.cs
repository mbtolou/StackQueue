#define MAX_PROC
#define T_SLICE

namespace StackQueue
{
    /// <summary>
    /// وقفه پیشرفت یک پردازش
    /// هرگاه در یک پردازش یک واحد پیشروی داشته باشیم ، با این وقفه به کلاس دیگر گزارش داده می شود
    /// </summary>
    /// <param name="p"></param>
    public delegate void OnProcessValuesChanged(ProcessJob p);

    /// <summary>
    /// متوط زمان اجرای پردازش ها 
    /// </summary>
    public struct JobMeans
    {
        /// <summary>
        /// متوسط زمان انتظار
        /// </summary>
        public double MeanWaitTime
        { get; set; }

        /// <summary>
        /// متوسط زمان خدمات دهی به پردازش
        /// </summary>
        public double MeanVsService
        { get; set; }
    }

    /// <summary>
    /// صف پردازش ، که خود نگهدارنده شماره پردازش و زمان باقیمانده و زمان سرویس می باشد
    /// </summary>
    public class JobQueue
    {
        public JobQueue Next;
        public int ProcessIndex;
        public int RemainTime;
        public double ResponceRatio;
        public int ServiceTime;
    }

    /// <summary>
    /// کلاس اصلی برای پردازش صف
    /// </summary>
    public class JobSchadules
    {
        public int NumOfProcesses = 5;
        public ProcessJob[] p_Backup = null;
        public ProcessJob[] processTable = null;
        public int TimeSlice = 1;
        public event OnProcessValuesChanged ProcessChanged;

        public event OnProcessValuesChanged ProcessCompeleted;

        public bool ArrivalTimeInZero { get; set; }

        /// <summary>
        /// First Com ,First Serve
        /// اولین ورود ، اولین سرویس گیرنده
        /// </summary>
        /// <returns></returns>
        public JobMeans FCFS()
        {
            JobMeans res = new JobMeans();
            int i = 0;
            double mean_turna = 0.0;
            double mean_vs = 0.0;
            int count_s = 0;
            int count_p = 0;
            ResetData();

            ProcessJob pj = null;

            while (count_s < NumOfProcesses)
            {
                count_p++;
                /*
                 * چک میشود اگر زمان ورود پردازشی از زمان
                 * جاری گذشته بود و پردازنده خالی بود
                 * آنرا در صف سرویس قرار می دهد
                 */
                if (pj == null && processTable[i].ArrivalTime <= count_p)
                {
                    pj = processTable[i];
                    i++;
                }

                if (pj != null)
                {
                    // به پردازش فعلی یک واحد خدمات می دهد
                    pj.RemainTime--;
                    /*
                     * زمان سرویس اگر تمام شده بود 
                     * برخی مشخصات پردازش را اصلاح می نماید
                     * این الگوریتم را می شد از این  
                     * بهینه تر نوشت ولی به خاطر اینکه می خواستم
                     * برای هر پردازش روی فرم اصلی نمایش دهم
                     * اینطوری نوشتم
                     */

                    if (pj.RemainTime <= 0)
                    {
                        pj.FinishTime = count_p;
                        pj.WaitTime = pj.FinishTime - pj.ArrivalTime;
                        mean_turna += (double)pj.WaitTime;
                        pj.Turna_vs_Service = (double)pj.WaitTime / pj.ServiceTime;
                        mean_vs += pj.Turna_vs_Service;
                        count_s++;
                        pj = null;
                    }
                    
                }
                else
                    continue;
            }

            /*
             * محاسبه مقادیر متوسط
             * این قسمت برای هم الگوریتم ها
             * مشترک است
             */
            mean_turna /= NumOfProcesses;
            mean_vs /= NumOfProcesses;
            res.MeanWaitTime = mean_turna;
            res.MeanVsService = mean_vs;

            return res;
        }

        /// <summary>
        /// تابع تولید اطلاعات تصادفی
        /// </summary>
        public void GenerateData()
        {
            processTable = new ProcessJob[NumOfProcesses];
            p_Backup = new ProcessJob[NumOfProcesses];

            for (int i = 0; i < NumOfProcesses; i++)
            {
                processTable[i] = new ProcessJob();
                processTable[i].ProcessChanged += this.ProcessChanged;
                processTable[i].ProcessCompeleted += this.ProcessCompeleted;
                processTable[i].ProcessId = i + 1;
                if (!ArrivalTimeInZero)
                    processTable[i].ArrivalTime = (i == 0) ? 0 : (i * 2) + RandomNumbers.NextNumber(3);
                processTable[i].ServiceTime = RandomNumbers.NextNumber(50);
                processTable[i].FinishTime = 0;
                processTable[i].RemainTime = processTable[i].ServiceTime;
                processTable[i].WaitTime = 0;
                processTable[i].Turna_vs_Service = 0.0;

                p_Backup[i] = processTable[i].Clone();
            }
        }


        /// <summary>
        /// Higest Response Ratio Next
        /// این الگوریتم پاسخگو ترین پردازش را 
        /// به عنوان پردازش فعلی انتخاب می کند
        /// </summary>
        /// <returns></returns>
        public JobMeans HRRN()
        {
            JobMeans res = new JobMeans();
            int i = 0;
            int j = 0;
            int k = 0;
            int cur_pos = 0;
            int highest_seq = 0;
            double highest_responce = 0.0;
            int[] seq = new int[NumOfProcesses];
            double responce_r = 0.0;
            int[] proc_fin = new int[NumOfProcesses];
            double mean_turna = 0.0;
            double mean_vs = 0.0;

            ResetData();

            for (i = 0; i < NumOfProcesses; i++)
            {
                /* پاسخگوترین پردازش را به عنوان پرداز فعلی انتخاب می نماید */
                highest_responce = -1;
                highest_seq = -1;

                /*
                 * برای رسیدن به پردازش فعال جستجو می کند 
                 */

                for (k = 0; k < NumOfProcesses; k++)
                    if (proc_fin[k] == 0)
                        break;

                /*
                 * محاسبه زمان پاسخگویی پردازش های فعال
                 * انتخاب پردازش فعال بعدی
                 */
                for (j = k; j < NumOfProcesses && proc_fin[j] == 0; j++)
                {
                    responce_r = (double)((cur_pos - processTable[j].ArrivalTime) + processTable[j].ServiceTime) / processTable[j].ServiceTime;

                    if (highest_responce < responce_r)
                    {
                        highest_responce = responce_r;
                        highest_seq = j;
                    }
                }

                /*
                 * ذخیره پردازش بعدی در لیست
                 */
                seq[i] = highest_seq;
                proc_fin[seq[i]] = 1;

                // سرویس به پردازش
                for (int y = 0; y < processTable[seq[i]].ServiceTime; y++)
                {
                    cur_pos++;
                    processTable[seq[i]].RemainTime--;
                }

                // محاسبات انتهای پردازش
                processTable[seq[i]].FinishTime = cur_pos;
                processTable[seq[i]].WaitTime = processTable[seq[i]].FinishTime - processTable[seq[i]].ArrivalTime;
                mean_turna += (double)processTable[seq[i]].WaitTime;
                processTable[seq[i]].Turna_vs_Service = (double)processTable[seq[i]].WaitTime / processTable[seq[i]].ServiceTime;
                mean_vs += processTable[seq[i]].Turna_vs_Service;
            }
            mean_turna /= NumOfProcesses;
            mean_vs /= NumOfProcesses;
            res.MeanWaitTime = mean_turna;
            res.MeanVsService = mean_vs;

            return res;
        }

        /// <summary>
        /// اطلاعات پردازش ها را ریست می کند
        /// </summary>
        public void ResetData()
        {
            processTable = new ProcessJob[NumOfProcesses];

            for (int i = 0; i < NumOfProcesses; i++)
            {
                processTable[i] = p_Backup[i].Clone();
            }
        }

        /// <summary>
        /// Round Robin
        /// در این الگوریتم زمان بین پردازش های فعال تقسیم می شود
        /// </summary>
        /// <returns></returns>
        public JobMeans RR()
        {
            JobMeans res = new JobMeans();
            int i = 0;
            int j = 0;
            int cur_pos = 0;
            int count_s = 0;
            int count_p = 0;
            double mean_turna = 0.0;
            double mean_vs = 0.0;
            JobQueue ready_queue = null;

            ResetData();

            while (count_s < NumOfProcesses)
            {
                /*
                 * انتخاب پردازش فعال
                 * افزودن به لیست پردازش ها
                 */
                if (i < NumOfProcesses && processTable[i].ArrivalTime <= cur_pos)
                {
                    ready_queue = AddToQueue(ready_queue, i);
                    count_p++;
                    i++;
                }

                if (ready_queue == null)
                {
                    cur_pos++;
                    continue;
                }

                /*
                 * این الگوریتم برای تکه زمانی یک طراحی شده است
                 * در صورت موجود بودن پردازش یک واحد زمان خدمات می گیرد
                 */
                for (j = 0; j < TimeSlice && ready_queue.ServiceTime < processTable[ready_queue.ProcessIndex].ServiceTime; j++)
                {
                    ready_queue.ServiceTime++;
                    processTable[ready_queue.ProcessIndex].RemainTime--;
                    cur_pos++;
                }

                /*
                 * اگر به میزان درخواستی سرویس گرفته بود 
                 * از صف خارج و محاسبات لازم برای آن انجام می گیرد
                 */
                if (ready_queue.ServiceTime == processTable[ready_queue.ProcessIndex].ServiceTime)
                {
                    processTable[ready_queue.ProcessIndex].FinishTime = cur_pos;
                    processTable[ready_queue.ProcessIndex].WaitTime = processTable[ready_queue.ProcessIndex].FinishTime - processTable[ready_queue.ProcessIndex].ArrivalTime;
                    mean_turna += (double)processTable[ready_queue.ProcessIndex].WaitTime;
                    processTable[ready_queue.ProcessIndex].Turna_vs_Service = (double)processTable[ready_queue.ProcessIndex].WaitTime / processTable[ready_queue.ProcessIndex].ServiceTime;
                    mean_vs += processTable[ready_queue.ProcessIndex].Turna_vs_Service;
                    /*از صف خارج شده و پردازش قیلی جایگزین آن میشود */
                    ready_queue = TakeFromQueue(ready_queue);
                    count_p--;
                    count_s++;
                    continue;
                }

                if (count_p > 1)
                {
                    /*جابجایی بین پردازش های فعال به صورت چرخشی*/
                    ready_queue = MoveQueue(ready_queue);
                }
            }
            mean_turna /= NumOfProcesses;
            mean_vs /= NumOfProcesses;
            res.MeanWaitTime = mean_turna;
            res.MeanVsService = mean_vs;

            return res;
        }

        /// <summary>
        /// Shortest Job First
        /// کوتاه ترین پردازش در ابتدا
        /// </summary>
        /// <returns></returns>
        public JobMeans SRT()
        {
            JobMeans res = new JobMeans();
            int i = 0;
            int j = 0;
            int k = 0;
            int cur_pos = 0;
            int shortest = 0;
            int shortest_id = 0;
            int count_s = 0;
            int count_p = 0;
            int flag = 0;
            double mean_turna = 0.0;
            double mean_vs = 0.0;
            JobQueue ready_queue = null;
            JobQueue cur_node = null;

            ResetData();

            while (count_s < NumOfProcesses)
            {
                /*
                 * در صورت رسیدن زمان خدمات گیری در صف قرار می گیرد
                 */
                if (i < NumOfProcesses && processTable[i].ArrivalTime <= cur_pos)
                {
                    ready_queue = AddToQueue(ready_queue, i);
                    count_p++;
                    i++;

                    cur_node = ready_queue;
                    shortest_id = cur_node.ProcessIndex;
                    shortest = cur_node.RemainTime;
                    cur_node = cur_node.Next;
                    /* جستجو برای یافتن کوتاه ترین پردازش */
                    while (cur_node != null)
                    {
                        if (shortest > cur_node.RemainTime)
                        {
                            shortest_id = cur_node.ProcessIndex;
                            shortest = cur_node.RemainTime;
                        }
                        cur_node = cur_node.Next;
                    }

                    /*چرخش صف برای یافتن پردازش مد نظر*/
                    while (ready_queue.ProcessIndex != shortest_id)
                        if (count_p > 1)
                            ready_queue = MoveQueue(ready_queue);
                }

                cur_pos++;
                if (ready_queue == null)
                    continue;

                if (ready_queue.RemainTime > 0)
                {
                    ready_queue.RemainTime--;
                    processTable[ready_queue.ProcessIndex].RemainTime--;
                }

                /*
                 * محاسبات پردازش
                 */
                if (ready_queue.RemainTime == 0)
                {
                    processTable[ready_queue.ProcessIndex].FinishTime = cur_pos;
                    processTable[ready_queue.ProcessIndex].WaitTime = processTable[ready_queue.ProcessIndex].FinishTime - processTable[ready_queue.ProcessIndex].ArrivalTime;
                    mean_turna += (double)processTable[ready_queue.ProcessIndex].WaitTime;
                    processTable[ready_queue.ProcessIndex].Turna_vs_Service = (double)processTable[ready_queue.ProcessIndex].WaitTime / processTable[ready_queue.ProcessIndex].ServiceTime;
                    mean_vs += processTable[ready_queue.ProcessIndex].Turna_vs_Service;
                    ready_queue = TakeFromQueue(ready_queue);
                    count_p--;
                    count_s++;
                }
            }
            mean_turna /= NumOfProcesses;
            mean_vs /= NumOfProcesses;
            res.MeanWaitTime = mean_turna;
            res.MeanVsService = mean_vs;

            return res;
        }

        /// <summary>
        /// Add to Queue
        /// افزودن به صف پردازش ها و قرار دهی در ابتدای صف
        /// </summary>
        /// <param name="ready_queue"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private JobQueue AddToQueue(JobQueue ready_queue, int index)
        {
            JobQueue node = null;
            JobQueue cur_node = ready_queue;

            node = new JobQueue();
            node.ProcessIndex = index;
            node.RemainTime = processTable[index].ServiceTime;
            node.ResponceRatio = 0.0;
            node.ServiceTime = 0;

            if (ready_queue == null)
            {
                ready_queue = node;
                node.Next = null;
                return ready_queue;
            }

            node.Next = ready_queue;
            ready_queue = node;
            return ready_queue;
        }

        /// <summary>
        /// شیفت پردازش ها به صورت حلقوی
        /// </summary>
        /// <param name="ready_queue"></param>
        /// <returns></returns>
        private JobQueue MoveQueue(JobQueue ready_queue)
        {
            JobQueue cur_node = ready_queue;
            JobQueue next_node = cur_node.Next;

            /* Moves last node into begining of the list. */
            while (next_node.Next != null)
            {
                cur_node = next_node;
                next_node = cur_node.Next;
            }

            cur_node.Next = null;
            next_node.Next = ready_queue;
            ready_queue = next_node;

            return ready_queue;
        }

        /// <summary>
        /// حذف پردازش از صف و اصلاح صف
        /// </summary>
        /// <param name="ready_queue"></param>
        /// <returns></returns>
        private JobQueue TakeFromQueue(JobQueue ready_queue)
        {
            JobQueue cur_queue = ready_queue;

            ready_queue = ready_queue.Next;
            cur_queue.Next = null;

            return ready_queue;
        }
    }


    /// <summary>
    /// کلاس پردازش با مشخصه های مد نظر 
    /// در هر مشخسه برای گزارش تغییر از یک وقفه استفاده گردیده است
    /// </summary>
    public class ProcessJob
    {
        private int _ft = 0;

        private int _rt = 0;

        private int _tt = 0;

        private double _tvs = 0;

        public int ArrivalTime
        { get; set; }

        public int FinishTime
        { get { return _ft; } set { _ft = value; if (ProcessCompeleted != null) ProcessCompeleted(this); } }

        public OnProcessValuesChanged ProcessChanged
        { get; set; }

        public OnProcessValuesChanged ProcessCompeleted
        { get; set; }

        public int ProcessId
        { get; set; }
        public int RemainTime
        { get { return _rt; } set { _rt = value; if (ProcessChanged != null) ProcessChanged(this); } }

        public int ServiceTime
        { get; set; }
        public double Turna_vs_Service
        { get { return _tvs; } set { _tvs = value; if (ProcessChanged != null) ProcessChanged(this); } }

        public int WaitTime
        { get { return _tt; } set { _tt = value; if (ProcessChanged != null) ProcessChanged(this); } }


        public ProcessJob Clone()
        {
            return new ProcessJob
            {
                ArrivalTime = this.ArrivalTime,
                FinishTime = this.FinishTime,
                ProcessId = this.ProcessId,
                RemainTime = this.RemainTime,
                ServiceTime = this.ServiceTime,
                Turna_vs_Service = this.Turna_vs_Service,
                WaitTime = this.WaitTime,
                ProcessChanged = this.ProcessChanged,
                ProcessCompeleted = this.ProcessCompeleted
            };
        }
    }
}