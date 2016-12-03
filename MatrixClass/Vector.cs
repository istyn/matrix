using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixClass
{
    class Vector : Matrix
    {
        public List<double> endpoint;
        private double magnitude;
        public bool IsUnitVector = false;
        public Vector(int dimensions)
        {
            magnitude = dimensions;
            endpoint = new List<double>(dimensions);
        }
        public Vector(Matrix v)                     //COPY CONSTRUCTOR for a 1xN matrix
        {
            if (v.N == 1 || v.M == 1)               //if matrix dimension is 1 by some number
            {                
                magnitude = Math.Max(v.M, v.N);
                
                initVector(endpoint.Count,v.Values);
            }
            else
                throw new Exception("Matrix A is not a vector!");   //defensive programming
        }
        public Vector(double[] components)
        {
            magnitude = components.Length;
            endpoint = new List<double>(components.Length);
            
            for (int i = 0; i <= components.GetUpperBound(0); i++)
            {
                //base.Values[i, 0] = components[i];//map each
                endpoint.Add(components[i]);
            }
        }

        private void initVector(int dim, double[,] components)
        {
            if (dim > 1 && dim - 1 == components.GetUpperBound(0))//if at least in R2 with corresponding components
            {
                base.Values = new double[dim, 1];             //store vector as column vectors, by default
                this.endpoint = new List<double>(dim);
                for (int i = 0; i < dim; i++)
                {                                           //deep copy components in array to Vector
                    base.Values[i, 0] = components[i, 0];
                }
                magnitude = MatrixNorm();
                base.M = components.GetUpperBound(0) + 1;
                base.N = 1;//because column vectors as default
            }
        }
        public bool getInUnitSpace()
        {
            double newMagnitude = 0;
            for (int i = 0; i < endpoint.Count; i++)
            {
                if (endpoint[i]>newMagnitude)
                {
                    newMagnitude = endpoint[i];
                }
            }
            for (int i = 0; i < endpoint.Count; i++)
            {
                endpoint[i] = endpoint[i] / magnitude;
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
}
