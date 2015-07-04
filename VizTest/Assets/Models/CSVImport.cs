using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using VisPOC;

namespace ABCTVBroadcast.Models
{
    public class CSVImport
    {
        public string CSVFile { get; set; }

		public List<VisPOC.BroadcastStruct> lst = new List<VisPOC.BroadcastStruct>();
        public int i;
        int j;
        public int showPos;
        public int episodeCount;

        public void StartImport()
        {
            string[] rows;
            string seriesTitle;
            string epiTitle;

            // PARSE
            using (StreamReader reader = new StreamReader(CSVFile))
            {
                reader.ReadLine(); //Skip the first line, which demonstrates the Data Structure.
                
                while(!reader.EndOfStream)
                {
                    var regex = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");

                    var rows_lst = new List<string>();
                    foreach (Match m in regex.Matches(reader.ReadLine()))
                        rows_lst.Add(cleanField(m.Value));
                    rows = rows_lst.ToArray();

					if (rows[6] != "")
					{
	                    seriesTitle = rows[6];
	                    epiTitle = rows[5];

	                    var newEpisode = new EpisodeStruct(
	                        ProgramName: epiTitle, 
	                        Region: rows[0], 
	                        Date: rows[1]
	                    );

	                    bool found=false;
						foreach (VisPOC.BroadcastStruct b in lst)
	                    {
	                        if (b.SeriesName == seriesTitle)
	                        {
	                            b.episodeList.Add(newEpisode);
	                            found = true;
	                            break;
	                        }
	                    }

	                    if (!found)
	                    {
							VisPOC.BroadcastStruct newSeries = new VisPOC.BroadcastStruct(seriesTitle, newEpisode);
	                        lst.Add(newSeries);
						}    
					}                
                }


            }

        }

        private string cleanField(string s)
        {
			if (s.StartsWith("\"") || s.StartsWith("'") || s.StartsWith(@""""))
            {
                s = s.Substring(1, s.Length - 1);
            }

			if (s.EndsWith("\"") || s.EndsWith("'") || s.EndsWith(@""""))
            {
                s = s.Substring(0, s.Length - 1);
            }

            s = s.Trim();
            s = s == "NULL" ? "" : s;

            return s;
        }
    }
}
