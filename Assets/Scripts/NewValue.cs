using Id = it.unical.mat.embasp.languages.Id;
using Param = it.unical.mat.embasp.languages.Param;

[Id("newValue")]
public class NewValue
{
    [Param(0)]
    private int row;
    [Param(1)]
    private int column;
    [Param(2)]
    private int value;

    public NewValue(int r, int c, int v)
    {
        this.row = r;
        this.column = c;
        this.value = v;
    }

    public NewValue()
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
