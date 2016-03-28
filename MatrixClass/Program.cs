/////////////////////////////////////////////////////////////////////////////////////////////////
//
// Name:                        Isaac Styles
// Department Name      : Computer and Information Sciences 
// File Name                 : Program.cs
// Purpose		       : Parses array of doubles and performs either addition or multiplication
//
//							
// Author			: Isaac Styles, styles@goldmail.etsu.edu
// Create Date		: October 2, 2014
//
//-----------------------------------------------------------------------------------
//
// Modified Date	: October 11, 2014
// Modified By		: Isaac Styles
//
///////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixClass
{
    class Program
    {
        static void Main(string[] args)
        {
            /*double[,] valuesA = new double[,]
            {
                {1.0,0.0,0.0,10.0},
                {1.0,1.0,0.0,10.0},
                {1.0,0.0,1.0,4.0}
            };            
            double[,] valuesI = new double[2,2]
            {
                {1.0,0.0},
                {0.0,1.0}
            };
            double[,] valuesB = new double[,]
            {
                {1.0,1.0,1.0,2.0},
                {3.0,1.0,1.0,0.0},
                {1.0,1.0,1.0,1.0}
            };

            //testing my various Constructor classes
            Matrix A = new Matrix(3,1,valuesA);
            Matrix I = new Matrix(valuesI);   //I is 2x2 identity matrix
            Matrix B = new Matrix(1,3,valuesB);
            Matrix C = new Matrix();
             */

            //Matrix D = new Matrix(new double[3,2]{{1.0},{2.0},{3.0}});
            //Matrix E = new Matrix(new double[3,2] { { 4.0 }, { 5.0 }, { 6.0 } });


            //Matrix.TryAddition(A, I, out C);
            //A.print();
            //B.print();
            //Matrix.TryMultiplication(A, I, out C);
            // System.Console.WriteLine(I.ToString());

            Matrix a, b, c;
            string line = Console.ReadLine();
            if (line == "add")
            {
                string[] lines;                                             //holds split string of line
                int[] dimensions = new int[2];                              //2-D array
                lines = Console.ReadLine().Split(new char[] { ' ' });       //read first line
                dimensions[0] = Int32.Parse(lines[0]);                      //parse dims
                dimensions[1] = Int32.Parse(lines[1]);


                a = Program.ParseArray(dimensions);             //should read lines containing first matrix of the operation

                lines = Console.ReadLine().Split(new char[] { ' ' });       //read second matrix dims
                dimensions[0] = Int32.Parse(lines[0]);                      //parse dims
                dimensions[1] = Int32.Parse(lines[1]);

                b = Program.ParseArray(dimensions);             //should read lines containing second matrix of operation
                c=a+b;
                if (c!=null)
                {
                    c.Print();
                }
                else
                {
                    Console.WriteLine("Matrix addition is not possible. Check bounds!");
                }
                
            }
            else if (line == "multiply")
            {
                string[] lines;                                             //holds split string of line
                int[] dimensions = new int[2];                              //2-D array
                lines = Console.ReadLine().Split(new char[] { ' ' });       //read first line
                dimensions[0] = Int32.Parse(lines[0]);                      //parse dims
                dimensions[1] = Int32.Parse(lines[1]);


                a = Program.ParseArray(dimensions);             //should read lines containing first matrix of the operation

                lines = Console.ReadLine().Split(new char[] { ' ' });       //read second matrix dims
                dimensions[0] = Int32.Parse(lines[0]);                      //parse dims
                dimensions[1] = Int32.Parse(lines[1]);

                b = Program.ParseArray(dimensions);             //should read lines containing second matrix of operation
                c = a * b;
                if (c!=null)
                {
                    c.Print();
                }
                else
                {
                    Console.WriteLine("Multiplication not possible. Try checking interior dimensions!");
                }

                //System.Console.WriteLine("Is Orthogonal: " + Matrix.IsOrthogonal(A, I));
            }
        }

        private static Matrix ParseArray(int[] dims)
        {
            Matrix a = new Matrix(dims[0], dims[1]);
            for (int i = 0; i < dims[0]; i++)
            {
                string[] l = Console.ReadLine().Split(new char[] { ' ' });
                for (int j = 0; j < dims[1]; j++)
                {
                    a.setValue(i, j, Double.Parse(l[j]));

                }
            }

            //a = new Matrix(dims[0], dims[1], ref lines[arrayIndex]);
            return a;
        }

    }
}
