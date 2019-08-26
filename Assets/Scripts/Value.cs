using Id = it.unical.mat.embasp.languages.Id;
using Param = it.unical.mat.embasp.languages.Param;

[Id("value")]
public class Value
{
    [Param(0)]
    private int row;
    [Param(1)]
    private int column;
    [Param(2)]
    private int value;

    public Value(int r, int c, int v)
    {
        this.row = r;
        this.column = c;
        this.value = v;
    }

    public Value()
    {
    }

    public int getRow()
    {
        return row;
    }

    public void setRow(int row)
    {
        this.row = row;
    }

    public int getColumn()
    {
        return column;
    }

    public void setColumn(int column)
    {
        this.column = column;
    }

    public int getValue()
    {
        return value;
    }

    public void setValue(int value)
    {
        this.value = value;
    }

}
