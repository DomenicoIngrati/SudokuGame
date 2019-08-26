using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using it.unical.mat.embasp.@base;
using it.unical.mat.embasp.languages.asp;
using it.unical.mat.embasp.specializations.incrementalIDLV.desktop;



public class SudokuSolver 
{

    private static string encodingResource9x9 = @"C:\Users\domei\Desktop\SudokuTesi\SudokuGame\Assets\Encodings\sudoku9x9.dl";
    private static string encodingResource16x16 = @"C:\Users\domei\Desktop\SudokuTesi\SudokuGame\Assets\Encodings\sudoku16x16.dl";
    private static string grounderResource = @"C:\Users\domei\Desktop\SudokuTesi\SudokuGame\Assets\lib\idlv.exe";
    private static string solverResource = @"C:\Users\domei\Desktop\SudokuTesi\SudokuGame\Assets\lib\clingo.exe";
    private static string encodingResource;

    private static Handler handler;
    private static int N;
    int[,] riddleGrid;

    List<Value> newValues = new List<Value>();
    


    public SudokuSolver(int[,] grid,int n)
    {
        N = n;

        if (N == 9)
        {
            encodingResource = encodingResource9x9;
        }
        else
        {
            encodingResource = encodingResource16x16;
        }

        riddleGrid = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            for(int j = 0; j < N; j++)
            {
                riddleGrid[i, j] = grid[i, j];
            }
        }

        handler = new IDLVDesktopHandler(new IDLVDesktopService(grounderResource, solverResource));
        
        
        SetPrograms();
    }


    public void SetPrograms()
    {
        

        // register the class for reflection
        try
        {
            ASPMapper.Instance.RegisterClass(typeof(NewValue));

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }

    }

    public int[,] SolveMatrix()
    {
        handler.RemoveAll();

        InputProgram facts = new ASPInputProgram();
        InputProgram encoding= encoding = new ASPInputProgram();

        for (int i = 0; i < N; i++)
        {

            for (int j = 0; j < N; j++)
            {

                if (riddleGrid[i, j] != 0)
                {
                    try
                    {
                        facts.AddObjectInput(new Value(i, j, riddleGrid[i, j]));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                }

            }

        }

       

        encoding.AddFilesPath(encodingResource);

        handler.AddProgram(facts);
        handler.AddProgram(encoding);

        Output o = handler.StartSync();

        AnswerSets answers = (AnswerSets)o;
        foreach (AnswerSet a in answers.Answersets)
        {
            int conta = 0;
            try
            {
                foreach (object obj in a.Atoms)
                {
                    if (!(obj is NewValue))
                    {
                        continue;
                    }
                    conta++;

                    
                    NewValue newValue = (NewValue)obj;
                    riddleGrid[newValue.getRow(),newValue.getColumn()] = newValue.getValue();
                    
                }

                //Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

        }


        
        return riddleGrid;

    }
}
