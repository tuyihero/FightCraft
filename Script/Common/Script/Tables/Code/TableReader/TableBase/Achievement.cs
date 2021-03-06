﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

using UnityEngine;

namespace Tables
{
    public partial class AchievementRecord  : TableRecordBase
    {
        public DataRecord ValueStr;

        public override string Id { get; set; }        public string Name { get; set; }
        public string Desc { get; set; }
        public string ClassScript { get; set; }
        public int AchieveTarget { get; set; }
        public bool ClassFinal { get; set; }
        public AchievementRecord(DataRecord dataRecord)
        {
            if (dataRecord != null)
            {
                ValueStr = dataRecord;
                Id = ValueStr[0];

            }
        }
        public override string[] GetRecordStr()
        {
            List<string> recordStrList = new List<string>();
            recordStrList.Add(TableWriteBase.GetWriteStr(Id));
            recordStrList.Add(TableWriteBase.GetWriteStr(Name));
            recordStrList.Add(TableWriteBase.GetWriteStr(Desc));
            recordStrList.Add(TableWriteBase.GetWriteStr(ClassScript));
            recordStrList.Add(TableWriteBase.GetWriteStr(AchieveTarget));
            recordStrList.Add(TableWriteBase.GetWriteStr(ClassFinal));

            return recordStrList.ToArray();
        }
    }

    public partial class Achievement : TableFileBase
    {
        public Dictionary<string, AchievementRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public AchievementRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("Achievement" + ": " + id, ex);
            }
        }

        public Achievement(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, AchievementRecord>();
            if(isPath)
            {
                string[] lines = File.ReadAllLines(pathOrContent);
                lines[0] = lines[0].Replace("\r\n", "\n");
                ParserTableStr(string.Join("\n", lines));
            }
            else
            {
                ParserTableStr(pathOrContent.Replace("\r\n", "\n"));
            }
        }

        private void ParserTableStr(string content)
        {
            StringReader rdr = new StringReader(content);
            using (var reader = new CsvReader(rdr))
            {
                HeaderRecord header = reader.ReadHeaderRecord();
                while (reader.HasMoreRecords)
                {
                    DataRecord data = reader.ReadDataRecord();
                    if (data[0].StartsWith("#"))
                        continue;

                    AchievementRecord record = new AchievementRecord(data);
                    Records.Add(record.Id, record);
                }
            }
        }

        public void CoverTableContent()
        {
            foreach (var pair in Records)
            {
                pair.Value.Name = TableReadBase.ParseString(pair.Value.ValueStr[1]);
                pair.Value.Desc = TableReadBase.ParseString(pair.Value.ValueStr[2]);
                pair.Value.ClassScript = TableReadBase.ParseString(pair.Value.ValueStr[3]);
                pair.Value.AchieveTarget = TableReadBase.ParseInt(pair.Value.ValueStr[4]);
                pair.Value.ClassFinal = TableReadBase.ParseBool(pair.Value.ValueStr[5]);
            }
        }
    }

}