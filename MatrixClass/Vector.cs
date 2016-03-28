/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixClass
{
    class Vector : Matrix
    {
        public int dimension;
        private double magnitude;
        public bool IsUnitVector = false;
        public Vector()
        
        {
            
        }

        public Vector(Matrix v)
        {
            if (v.nCols == 1 || v.nRows == 1)               //if matrix dimension is 1 by some number
            {                
                dimension = Math.Max(v.nRows, v.nCols);
                
                if (magnitude == 1)                 //is the vector a unit vector?
                {
                    IsUnitVector= true;
                }
                if (dimension == v.nCols)              //if row vector, transpose to column vector
                {
                    v.Transpose();
                }
                initVector(dimension,v.m);


            }
            else
                throw new Exception("Matrix A is not a vector!");

            

        }

        public Vector(double[] components)
        {

            for (int i = 0; i <= components.GetUpperBound(0); i++)
            {
                base.m[i, 0] = components[i];//map each

            }
        }

        private void initVector(int dim, double[,] components)
        {
            if (dim > 1 && dim - 1 == components.GetUpperBound(0))//if at least in R2 with corresponding components
            {
                base.m = new double[dim, 1];             //store vector as column vectors, by default
                this.dimension = dim;
                for (int i = 0; i < dim; i++)
                {                                           //deep copy components in array to Vector
                    base.m[i, 0] = components[i, 0];
                }
                magnitude = MatrixNorm();
                base.nRows = components.GetUpperBound(0) + 1;
                base.nCols = 1;//because column vectors as default
            }
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}*/
