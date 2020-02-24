using System;
using Google.Protobuf.WellKnownTypes;
using Truescriber.DAL.Entities.Tasks;

namespace Truescriber.DAL.Entities
{
    public class Word
    {
        private Word()
        {
        }

        public Word(
            string value,
            Duration startTime,
            Duration finishTime,
            int index,
            int id
        )
        {
            Value = value;
            StartTime = startTime.ToTimeSpan();
            FinishTime = finishTime.ToTimeSpan();
            Index = index;
            TaskId = id;
        }
        public int Id { get; protected set; }
        public string Value { get; protected set; }
        public TimeSpan StartTime { get; protected set; }
        public TimeSpan FinishTime { get; protected set; }  

        public int Index { get; protected set; }

        public int TaskId { get; protected set; }
        public virtual Task Task { get; protected set; }
    }
}
