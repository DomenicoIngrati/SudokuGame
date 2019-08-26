using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Board16x16 : MonoBehaviour
{
    int[,] solvedGrid = new int[16, 16];
    string s;
    int[,] riddleGrid = new int[16, 16];
    

    public Transform A1, A2, A3,A4,B1, B2, B3,B4, C1, C2, C3,C4,D1,D2,D3,D4;

    public GameObject buttonPrefab;


    public SudokuSolver sudokuSolver;

    List<NumberField> fieldList = new List<NumberField>();

    // Start is called before the first frame update
    void Start()
    {
        
        InitGrid(ref riddleGrid);
        CreateButtons();
    }



    void InitGrid(ref int[,] grid) //INIZIALIZZA LA MATRICE CON I NUMERI
    {
        string[] lines = null;

        lines = File.ReadAllLines(@"Assets/Grid/16x16/grid" + 1 + ".txt");
        int k = 0;
        for(int i = 0; i < lines.Length; i++)
        {
            k = 0;
            for(int j = 0; j < lines[i].Length; j++)
            {
                
                if (lines[i][j].Equals(' '))
                    continue;

               string number = lines[i][j].ToString();


                if (j != lines[i].Length - 1) 
                    if (!lines[i][j+1].Equals(' '))
                    {
                        number += lines[i][j + 1].ToString();
                        j++;
                    }


                grid[i, k++] = int.Parse(number);
                
            }
        }

        sudokuSolver = new SudokuSolver(grid,16);
    }



    void DebugGrid(ref int[,] grid) //STAMPA LA MATRICE
    {
        s = "";
        
        for (int row = 0; row < 16; row++)
        {
            s += "|";
            for (int col = 0; col < 16; col++)
            {
                s += grid[row, col].ToString();
                s += " ";

            }
            s += "\n";
        }

        print(s);
    }


    void CreateButtons() //CREIAMO I BOTTONI
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                GameObject newButton = Instantiate(buttonPrefab);


                //SETTIAMO I VALORI
                NumberField numberField = newButton.GetComponent<NumberField>();
                numberField.SetValue(i, j, riddleGrid[i, j], i + "," + j);
                newButton.name = i + "," + j;

                //RIEMPIAMO LA FIELDLIST CON I FIELD CHE HANNO VALORE 0

                if (riddleGrid[i, j] == 0)
                {
                    fieldList.Add(numberField);
                }

                //COLLEGHIAMO I BOTTONI

                //A1
                if (i < 4 && j < 4)
                {
                    newButton.transform.SetParent(A1, false);
                }

                //A2
                if (i < 4 && j > 3 && j < 8)
                {
                    newButton.transform.SetParent(A2, false);
                }

                //A3
                if (i < 4 && j > 7 && j< 12)
                {
                    newButton.transform.SetParent(A3, false);
                }

                //A4
                if (i < 4 && j > 11)
                {
                    newButton.transform.SetParent(A4, false);
                }

                //B1
                if (i >3 && i<8 && j < 4)
                {
                    newButton.transform.SetParent(B1, false);
                }

                //B2
                if (i > 3 && i < 8 && j > 3 && j < 8)
                {
                    newButton.transform.SetParent(B2, false);
                }

                //B3
                if (i > 3 && i < 8 && j > 7 && j < 12)
                {
                    newButton.transform.SetParent(B3, false);
                }

                //B4
                if (i > 3 && i < 8 && j > 11)
                {
                    newButton.transform.SetParent(B4, false);
                }

                //C1
                if (i > 7 && i < 12 && j < 4)
                {
                    newButton.transform.SetParent(C1, false);
                }

                //C2
                if (i > 7 && i < 12 && j > 3 && j < 8)
                {
                    newButton.transform.SetParent(C2, false);
                }

                //C3
                if (i > 7 && i < 12 && j > 7 && j < 12)
                {
                    newButton.transform.SetParent(C3, false);
                }

                //C4
                if (i > 7 && i < 12 && j > 11)
                {
                    newButton.transform.SetParent(C4, false);
                }

                //D1
                if (i>11 && j < 4)
                {
                    newButton.transform.SetParent(D1, false);
                }

                //D2
                if (i > 11 && j > 3 && j < 8)
                {
                    newButton.transform.SetParent(D2, false);
                }

                //D3
                if (i > 11 && j > 7 && j < 12)
                {
                    newButton.transform.SetParent(D3, false);
                }

                //D4
                if (i > 11 && j > 11)
                {
                    newButton.transform.SetParent(D4, false);
                }

            }
        }
    }


    public void SolveGrid()
    {
        //string s = "";

        //for(int i = 0; i < 16; i++)
        //{
        //    for(int j = 0; j < 16; j++)
        //    {
        //        if (riddleGrid[i, j] != 0)
        //        {
        //            s += "value(" + i + "," + j + "," + riddleGrid[i, j] + ").";
        //            s += "\n";
        //        }
        //    }
        //}

        //print(s);

        int cont = 0;
        solvedGrid = sudokuSolver.SolveMatrix();
        for (int k = 0; k < fieldList.Count; k++)
        {
           if (solvedGrid[fieldList[k].getX(), fieldList[k].getY()] != 0)
            {
                print("FOUND A SEMI-SOLUTION. PRESS SOLVE AGAIN");
                fieldList[k].SetHint(solvedGrid[fieldList[k].getX(), fieldList[k].getY()]);
                riddleGrid[fieldList[k].getX(), fieldList[k].getY()] = solvedGrid[fieldList[k].getX(), fieldList[k].getY()];
                fieldList.RemoveAt(k);
                cont++;
            }
        }

        if (cont == 0)
        {
            print("NO SOLUTION");
        }
    }



    bool GridComplete()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (riddleGrid[i, j] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
