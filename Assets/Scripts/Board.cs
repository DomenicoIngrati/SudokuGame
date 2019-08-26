using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Board : MonoBehaviour
{

    int[,] solvedGrid = new int[9, 9];
    string s;
    int[,] riddleGrid = new int[9, 9];

    public Transform A1, A2, A3, B1, B2, B3, C1, C2, C3;

    public GameObject buttonPrefab;

    public SudokuSolver sudokuSolver;

    List<NumberField> fieldList = new List<NumberField>();
    

    void Start()
    {
        
        InitGrid(ref riddleGrid);
        CreateButtons();
    }

    void InitGrid(ref int[,] grid) //INIZIALIZZA LA MATRICE CON I NUMERI
    {
        string[] lines = null;

        lines = File.ReadAllLines(@"Assets/Grid/9x9/grid" + 1 + ".txt");

        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                grid[i, j] = int.Parse(lines[i][j].ToString());
            }
        }

        sudokuSolver = new SudokuSolver(grid,9);
    }

    void DebugGrid(ref int[,] grid) //STAMPA LA MATRICE
    {
        s = "";
        int sep = 0;
        for (int row = 0; row < 9; row++)
        {
            s += "|";
            for (int col = 0; col < 9; col++)
            {
                s += grid[row, col].ToString();
                sep = col % 3;

                if (sep == 2)
                {
                    s += "|";
                }
                
            }
            s += "\n";
        }

        print(s);


    }

   

    void CreateButtons() //CREIAMO I BOTTONI
    {
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
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
                if(i<3 && j < 3)
                {
                    newButton.transform.SetParent(A1, false);
                }

                //A2
                if (i < 3 && j > 2 && j<6)
                {
                    newButton.transform.SetParent(A2, false);
                }

                //A3
                if (i < 3 && j > 5)
                {
                    newButton.transform.SetParent(A3, false);
                }

                //B1
                if (i > 2 && i < 6  && j < 3)
                {
                    newButton.transform.SetParent(B1, false);
                }

                //B2
                if (i > 2 && i < 6 && j > 2 && j < 6)
                {
                    newButton.transform.SetParent(B2, false);
                }

                //B3
                if (i > 2 && i < 6 && j > 5)
                {
                    newButton.transform.SetParent(B3, false);
                }

                //C1
                if (i > 5 && j < 3)
                {
                    newButton.transform.SetParent(C1, false);
                }

                //C2
                if (i > 5 && j > 2 && j < 6)
                {
                    newButton.transform.SetParent(C2, false);
                }

                //C3
                if (i > 5 && j > 5)
                {
                    newButton.transform.SetParent(C3, false);
                }
            }
        }
    }


    





    public void SolveGrid()
    {
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
        for(int i = 0;i < 9; i++){
            for(int j = 0; j < 9; j++)
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
