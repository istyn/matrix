/////////////////////////////////////////////////////////////////////////////////////////////////
//
// Name:                Isaac Styles
// Department Name      : Computer and Information Sciences 
// File Name            : Matrix.cs
// Purpose		        : Establishes a Matrix whose array may be passed to in a Constructor,
//                              or set individually by helper method SetValue(int, int, double).
//                              Basic Matrix operations are Addition, Subtraction, Multiplication,
//                              and functions such as Transpose, Dot Product, Norm.
//							    Gaussian Elimination planned but unfinished.
// Author			    : Isaac Styles, styles@goldmail.etsu.edu
// Create Date		    : August 20, 2014
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
	class Matrix : IComparable
	{
        private int nRows;
        private int nCols;
		private double[,] m;
		

		public Matrix()
		{
			initMatrix(0, 0, new double[0, 0]);
		}

		public Matrix(int m, int n)
		{
			initMatrix(m, n);
		}

		public Matrix(int m, int n, double[,] values)
		{

			this.nRows = m;
			this.nCols = n;
			initMatrix(m, n, values);
		}

		public Matrix(double[,] values)
		{
			int m = values.GetUpperBound(0) + 1;
			int n = values.GetUpperBound(1) + 1;
			if (m >= 0)
			{
				if (n >= 0)
				{
					initMatrix(m, n, values);
				}
				else throw new Exception("Non-zero n dimension required");
			}
			else throw new Exception("Non-zero m dimension required");
			initMatrix(m, n, values);
		}
        public Matrix(int nRow, int nCol, ref string[] lines)
        {
            initMatrix(nRow, nCol);

            for (int r = 0; r < nRow; r++)
            {
                //l = Console.ReadLine();
                //string[] sL = lines[r].Split(new char[] { ' ' });
                for (int c = 0; c < nCol; c++)
                {
                    setValue(r, c, Double.Parse(lines[r]));
                }
            }
            //return C;
        }

		public Matrix(Matrix a) //copy constructor
		{
			initMatrix(a.nRows, a.nCols, a.m);
		}

        private void initMatrix(int m, int n)
        {
            this.m = new double[m, n];
            this.nRows = m;
            this.nCols = n;
            //deep copy of values[m,n] to Class Variables

        }

		private void initMatrix(int m, int n, double[,] values)
		{
			this.m = new double[m, n];
			this.nRows = m;
			this.nCols = n;
			//deep copy of values[m,n] to Class Variables
			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{

					this.m[i, j] = values[i, j];
				}
			}
		}

        public void setValue(int row, int col, double value)
        {
            m[row, col] = value;
        }

        public void Print()
        {
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {

                    Console.Write(m[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
        
		/*//Row-Echelon Form
		//TO DO: in place double[,] to improve usage of CPU cache (reference wiki:Gaussian elimination, wiki:BLAS)
		public static bool Reduce(Matrix A, out Matrix C)
		{
			int m = A.nRows;
			int n = A.nCols;
			//int columnPtr = 0;
			double[,] pivots;//holds a changing array of only pivots. 
			for (int i = 0, j=0; i <= m; i++, j++)
			{

				if (A.m[i, j] == 0)//if column needs a pivot
				{
					//bool suitablePivot = false;//NOTE FOR C++, do not use -1 for variable that is used to index array
					int CandidateForRowSwap =-1;//location of best row to swap, denoted r2
					//go looking for pivots in jth column, first preferring 1 (RREF shortcut), else returns the
					 // largest component which reduces the chance of repetitive division by a number very close
					 // to 1. This increases numeric stability in double precision division.     TO DO: Fraction Class
					//
                    for (int r2 = 0; r2 < A.nRows; r2++)
					{
						bool foundUnaryPivot = false;//no need to find more potential pivots if 1 is found
						if(A.m[r2,j]!=0)   //if found, swap so that [0,0] contains pivot
						{
							
							if (A.m[r2, j] == 1)       //pivot of 1 is ideal
							{
								CandidateForRowSwap = r2;
								foundUnaryPivot = true;      //, so stop searching
							}
							else
							{
								if (CandidateForRowSwap==-1)    //element is not 0,1, and we have not yet found a viable candidate
								{
									CandidateForRowSwap = r2;   //, so accept any non-zero pivot
								}
								else if (Math.Abs(A.m[r2, j]) > Math.Abs(A.m[CandidateForRowSwap, j]))//since there exists a viable candidate, prefer a pivot of larger magnitude
									{
										CandidateForRowSwap = r2;//found a pivot of larger magnitude; increases numeric stability
									}

							}
						}
						if (foundUnaryPivot == true)//get out of loop
							break;
					}

					if (CandidateForRowSwap != -1)//found a nonzero element of jth column
					{
						//double[,] c= new double[A.M,A.N];
						SwapRows(A.m, i, CandidateForRowSwap, out A.m);//found nonzero pivot, so swap it to ith row
						if (A.m[CandidateForRowSwap, j] != 1)
						{//double p=A.Values[i,j];
                            
							Matrix.DivideRow(A.m, i,A.m[i,j] , out A.m);
						}
						else
						{
							
						}
					}
					else//no nonzero elements in column ( all zeros )
					{
						j++;//increase the column pointer, eliminating the column of zeros
					}
				}
				else//found pivot
				{
				
				}

			}
		}*/

        #region reduce submethods
        /// <summary>
        /// Divides the elements
        /// </summary>
        /// <param name="A">A.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="m">The m.</param>
        /// <param name="c">The c.</param>
        private static void DivideRow(double[,] A, int rowIndex, double m, out double[,] c)
        {
            c = new double[A.GetUpperBound(0), A.GetUpperBound(1)];
            for (int i = 0; i < c.GetUpperBound(0); i++)
            {
                for (int j = 0; j < c.GetUpperBound(1); j++)
                {
                    if (i == rowIndex)
                    {
                        c[i, j] = A[i, j] / m;  //divide A[rowIndex,j] by double m
                    }
                    else
                        c[i, j] = A[i, j];  //deep copy A[,] into c[,]
                }
            }

        }

        private static void EliminateColumn(double[,] A, int m, int n, out double[,] c)
        {
            c = new double[m, n - 1];//eliminate the first column
            //columnPtr++;
            for (int i2 = 0; i2 < c.GetUpperBound(0); i2++)
            {
                for (int j2 = 0; j2 < c.GetUpperBound(1); j2++)
                {
                    c[i2, j2] = A[i2 + 1, j2 + 1];//deep copy A[m,n] -> c[m,n-1]
                }
            }

        }

        private static void SwapRows(double[,] A, int row1, int row2, out double[,] c)
        {
            c = new double[A.GetUpperBound(0), A.GetUpperBound(1)];
            double[] temp = new double[A.GetUpperBound(1)];
            for (int i = 0; i < c.GetUpperBound(0); i++)
            {
                for (int j = 0; j < c.GetUpperBound(1); j++)
                {
                    if (i == row1)
                    {
                        c[row2, j] = A[i, j];
                    }
                    else
                        c[i, j] = A[i, j];  //deep copy A[,] into c[,]
                }
            }
        }
        
        #endregion

		public void Transpose()
		{
			double[,] t = new double[this.nCols, this.nRows];
			for (int i = 0; i < nRows; i++)
			{
				for (int j = 0; j < nCols; j++)
				{
					t[j, i] = m[i, j];                 //swap m and n Class variables
				}
			}
			//Matrix b = new Matrix(this.N, this.M,t);//    m,n --> n,m
			m = t;                        
			this.nRows = t.GetUpperBound(0) + 1;
			this.nCols = t.GetUpperBound(1) + 1;
		}

		public double MatrixNorm()
		{
			double x = 0.0;
			for (int i = 0; i < this.nRows; i++)
			{
				for (int j = 0; j < this.nCols; j++)
				{
					x += Math.Pow(this.m[i, j], 2); //sum of values of A^2
				}

			}
			x = Math.Sqrt(x);                               //square root of sum(A[m,n]^2)
			return x;
		}

        #region Operators
        public static Matrix operator +(Matrix A, Matrix B)
        {
            Matrix C = null;

            if (!Matrix.TryAddition(A, B, out C))
            {
                Console.WriteLine("Addition not possible. Check bounds!");
            }
            return C;
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            Matrix C = null;

            if (!Matrix.TryMultiplication(A, B, out C))
            {
                Console.WriteLine("Multiplication not possible. Check bounds!");
            }
            return C;
        }
        #endregion

        //STATIC OPERATIONS
        //Addition A + B = C
        public static bool TryAddition(Matrix a, Matrix b, out Matrix c)
        {
            if (IsOneToOne(a, b))
            {
                c = new Matrix(a.m.GetLength(0), a.m.GetLength(1));
                for (int i = 0; i < a.nRows; i++)
                {
                    for (int j = 0; j < a.nCols; j++)
                    {
                        c.m[i, j] = a.m[i, j] + b.m[i, j];
                    }
                }
            }
            else
            {
                c = null;
                return false;
            }
            return true;
        }
        //Subtraction A - B = C
        public static bool TrySubtract(Matrix a, Matrix b, out Matrix c)
        {
            if (IsOneToOne(a, b))
            {
                c = new Matrix(a.m.GetLength(0), a.m.GetLength(1));
                for (int i = 0; i < a.nRows; i++)
                {
                    for (int j = 0; j < a.nCols; j++)
                    {
                        c.m[i, j] = a.m[i, j] - b.m[i, j];
                    }
                }
            }
            else
            {
                c = null;
                return false;
            }
            return true;
        }
        //Multiplication A * B = C
        public static bool TryMultiplication(Matrix a, Matrix b, out Matrix c)
        {
            if (a.nCols == b.nRows)                 //if inner dimensions allow for multiplication
            {
                c = new Matrix(a.nRows, b.nCols);   //new Am x Bn Matrix
                if (Matrix.IsOnToSquareMatrices(a, b) && c.nRows == 2) //case [2,2] x [2,2]
                {
                    double[] products = new double[7];               //using only 7 multiplications (strassen)
                    products[0] = a.m[0, 0] * (b.m[0, 1] - b.m[1, 1]);
                    products[1] = (a.m[0, 0] + a.m[0, 1]) * b.m[1, 1];
                    products[2] = (a.m[1, 0] + a.m[1, 1]) * b.m[0, 0];
                    products[3] = a.m[1, 1] * (b.m[1, 0] - b.m[0, 0]);
                    products[4] = (a.m[0, 0] + a.m[1, 1]) * (b.m[0, 0] + b.m[1, 1]);
                    products[5] = (a.m[0, 1] - a.m[1, 1]) * (b.m[1, 0] + b.m[1, 1]);
                    products[6] = (a.m[0, 0] - a.m[1, 0]) * (b.m[0, 0] + b.m[0, 1]);
                    //using 10 + 8 = 18 additions
                    c.m[0, 0] = products[4] + products[3] - products[1] + products[5];
                    c.m[0, 1] = products[0] + products[1];
                    c.m[1, 0] = products[2] + products[3];
                    c.m[1, 1] = products[4] + products[0] - products[2] - products[6];

                }
                else                            //O(n^3) matrix multiplication
                {
                    for (int i = 0; i < c.nRows; i++)
                    {
                        for (int j = 0; j < c.nCols; j++)
                        {
                            c.m[i, j] = 0;
                            for (int k = 0; k < a.nCols; k++)
                                c.m[i, j] = c.m[i, j] + a.m[i, k] * b.m[k, j];
                        }
                    }
                }

            }
            else
            {
                c = null;
                return false;
            }
            return true;
        }
        public static bool TryDotProduct(Matrix a, Matrix b, out double c)
        {
            bool valid = false;
            c = 0.0;                //holds the sum of products
            if (IsOneToOne(a, b))
            {
                double result = 0;
                for (int i = 0; i < a.nRows; i++)
                {
                    for (int j = 0; j < a.nCols; j++)
                    {
                        result = a.m[i, j] * b.m[i, j];
                        c += result;
                    }
                }
                valid = true;
            }
            else                   //check to see if there is a transposition discrepancy                            
            {
                b.Transpose();
                if (IsOneToOne(a, b))
                {
                    double result = 0;
                    for (int i = 0; i < a.nRows; i++)
                    {
                        for (int j = 0; j < a.nCols; j++)
                        {
                            result = a.m[i, j] * b.m[j, i];
                            c += result;
                        }
                    }
                    valid = true;
                }
            }
            return valid;
        }

		
		public int CompareTo(object obj)
		{
			int returnvalue=-1;                         //-1 is default, non equal result.
			if (obj is Matrix)
			{
				Matrix a = (Matrix)obj;                     //typecast obj as Matrix
				int x = 0;                                  //counter of identical components
				for (int i = 0; i < a.nRows; i++)
				{
					for (int j = 0; j < a.nCols; j++)
					{
						if (this.m[i, j] == a.m[i, j]) //compare corresponding values
						{ x++; }
						else break;
					}
				}
				if (x == m.Length)
				{
					returnvalue = 0;                             //all components are equal. return 0
				}
			}
			return returnvalue;
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
		public override string ToString()
		{
			string output = "";
			string row;
			for (int i = 0; i < nRows; i++)
			{
			   row = "";
				for (int j = 0; j < nCols; j++)
				{

					string num = m[i, j].ToString();
					num = num.PadLeft(5);
					row += num;

				}
				output+=row + "\n";

			}
			return output;
		}
		/*
		public static Vector isVector (Matrix a, out Vector v)
		{
			if (a.M == 1) //if matrix A is a horizontal vector, then
			{

			}
			else if (a.N == 1) //if matrix A is a vertical vector, then
			{

			}
		}*/
        #region Utilities

        public static bool IsOrthogonal(Matrix a, Matrix b)
        {
            double dotProduct;
            if (TryDotProduct(a, b, out dotProduct))
            {
                if (dotProduct == 0)
                    return true;

            }
            return false;
        }

        public static bool IsOnToSquareMatrices(Matrix a, Matrix b)
        {
            if (a.nRows == a.nCols && b.nRows == b.nCols)
            {
                return true;
            }
            return false;
        }

        public static bool IsOneToOne(Matrix a, Matrix b)
        {
            if (a.nRows == b.nRows && a.nCols == b.nCols)
            {
                return true;
            }
            return false;
        }

        public static bool IsTranspose(Matrix a, Matrix b)
        {
            bool returnValue = false;
            if (a.nRows == b.nCols && a.nCols == b.nRows)
            {
                b.Transpose();
                if (a == b)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        } 
        #endregion
	}
}   
