using NUnit.Framework;
using TestUnitaires;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests13_Determinants
    {
        [Test]
        [DefaultFloatingPointTolerance(0.001d)]
        public void TestDeterminantMatrix2x2()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            float determinant = MatrixFloat.Determinant(m);

            //GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(-2f, determinant);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        [DefaultFloatingPointTolerance(0.001d)]
        public void TestDeterminantMatrix3x3()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f },
            });

            float determinant = MatrixFloat.Determinant(m);

            //GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(0f, determinant);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        [DefaultFloatingPointTolerance(0.1d)]
        public void TestDeterminantMatrix4x4()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 0.707f, 2.449f, 4.243f, 1.000f },
                { 0.707f, 2.449f, -4.243f, 2.000f },
                { -1.732f, 2.000f, 0.000f, 3.000f },
                { 0.000f, 0.000f, 0.000f, 1.000f },
            });

            float determinant = MatrixFloat.Determinant(m);

            //GlobalSettings.DefaultFloatingPointTolerance = 0.1d;
            Assert.AreEqual(48f, determinant);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        [DefaultFloatingPointTolerance(0.001d)]
        public void TestDeterminantIdentityMatrices()
        {
            //Identity 2
            MatrixFloat identity2 = MatrixFloat.Identity(2);
            float determinantIdentity2 = MatrixFloat.Determinant(identity2);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(1f, determinantIdentity2);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
            
            //Identity 3
            MatrixFloat identity3 = MatrixFloat.Identity(3);
            float determinantIdentity3 = MatrixFloat.Determinant(identity3);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(1f, determinantIdentity3);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
            
            //Identity 10
            MatrixFloat identity10 = MatrixFloat.Identity(10);
            float determinantIdentity10 = MatrixFloat.Determinant(identity10);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(1f, determinantIdentity10);
            //GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }
}