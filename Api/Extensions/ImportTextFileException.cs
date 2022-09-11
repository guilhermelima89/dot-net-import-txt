using System.Runtime.Serialization;

namespace Api.Extensions;

[Serializable]
public class ImportTextFileException : Exception
{
    public int Line
    {
        get;
        private set;
    }
    public int Column
    {
        get;
        private set;
    }
    public ImportTextFileException() : base() { }
    public ImportTextFileException(string message) : base(message) { }
    public ImportTextFileException(string message, Exception innerException, int Line, int Column) : base(string.Format("Error in line {0} column {1}. {2}", Line, Column, message), innerException)
    {
        this.Line = Line;
        this.Column = Column;
    }
    protected ImportTextFileException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        this.Line = info.GetInt32("Line");
        this.Column = info.GetInt32("Column");
    }
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("Line", this.Line);
        info.AddValue("Column", this.Column);
    }
}