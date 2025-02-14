// using System;
// using System.Threading.Tasks;
// using TeslaACDC.Business.Interfaces;

// namespace TeslaACDC.Business.Services
// {
//     public class Matematica : IMatematica
//     {
//         public Task<float> SquareArea(float sideLength)
//         {
//             return Task.FromResult(sideLength * sideLength);
//         }

//         public Task<float> Sum(float value1, float value2)
//         {
//             return Task.FromResult(value1 + value2);
//         }

//         public Task Sum(object value1, object value2)
//         {
//             throw new NotImplementedException();
//         }

//         public Task<float> TriangleArea(float baseLength, float height)
//         {
//             return Task.FromResult(0.5f * baseLength * height);
//         }
//     }
// }
