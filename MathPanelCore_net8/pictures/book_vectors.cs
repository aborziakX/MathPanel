Vec3 vX = new Vec3(1, 0, 0);
Vec3 vY = new Vec3(0, 1, 0);
//векторное произведение, получаем единичный вектор вдоль Z
Vec3 vZ = Vec3.Product(vX, vY);
Dynamo.Console("vX =" + vX.ToString());
Dynamo.Console("vY =" + vY.ToString());
Dynamo.Console("vZ =" + vZ.ToString());
//масштабируем единичную матрицу
Mat3 m = new Mat3();
m.Scale(2, 1, 0.5);
Vec3 v = new Vec3(1, 1, 1);
Vec3 res = new Vec3();
//умножаем на вектор, результат в res
m.Mult(v, ref res);
Dynamo.Console("v =" + res.ToString());


