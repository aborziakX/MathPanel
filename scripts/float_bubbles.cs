//Александр Диков; dsasha0102@gmail.com
//пузырьки

const double g = 4; // Acceleration
const double DT = 0.05; // Step
Dynamo.ConsoleClear();
Dynamo.Console("float_bubbles");
Dynamo.SceneClear();

Random random = new Random();

// Make array of bubbles
int numBubbles = 15;
int[] initial_x = new int[numBubbles];
int[] initial_y = new int[numBubbles];
int[] initial_z = new int[numBubbles];
Phob[] bubbles = new Phob[numBubbles];

for (int i = 0; i < numBubbles; i++)
{
    initial_x[i] = (int)(random.NextDouble() * 30) + 5;
    initial_y[i] = (int)(random.NextDouble() * 30) + 5;
    initial_z[i] = 0;

    int id = Dynamo.PhobNew(initial_x[i], initial_y[i], initial_z[i]);
    bubbles[i] = Dynamo.PhobGet(id) as Phob;

    Sphere sphere = new Sphere((int)(random.NextDouble() * 3), "White", 16);
    bubbles[i].Shape = sphere;
}

// Water serface
int serface_lavel = 20;
int id2 = Dynamo.PhobNew(20, 20, serface_lavel);
var hz2 = Dynamo.PhobGet(id2) as Phob;
Cube parallelepiped = new Cube(2, "Blue");
parallelepiped.scaleX = 20;
parallelepiped.scaleY = 20;
parallelepiped.scaleZ = 0.1;
hz2.Shape = parallelepiped;

// Floor of aquarium
int id3 = Dynamo.PhobNew(20, 20, -2.5);
var floor = Dynamo.PhobGet(id3) as Phob;
Cube parallelepiped1 = new Cube(2, "Blue");
parallelepiped1.scaleX = 20;
parallelepiped1.scaleY = 20;
parallelepiped1.scaleZ = 0.5;
floor.Shape = parallelepiped1;

// Back wall
int id4 = Dynamo.PhobNew(20, 40, 10);
var backWall = Dynamo.PhobGet(id4) as Phob;
Cube parallelepiped2 = new Cube(2, "Blue");
parallelepiped2.scaleX = 20;
parallelepiped2.scaleY = 0.5;
parallelepiped2.scaleZ = 12;
backWall.Shape = parallelepiped2;

// Left wall
int id5 = Dynamo.PhobNew(0, 20, 10);
var leftWall = Dynamo.PhobGet(id5) as Phob;
Cube parallelepiped3 = new Cube(2, "Blue");
parallelepiped3.scaleX = 0.5;
parallelepiped3.scaleY = 20;
parallelepiped3.scaleZ = 12;
leftWall.Shape = parallelepiped3;

// Right wall
int id6 = Dynamo.PhobNew(40, 20, 10);
var rightWall = Dynamo.PhobGet(id6) as Phob;
Cube parallelepiped4 = new Cube(2, "Blue");
parallelepiped4.scaleX = 0.5;
parallelepiped4.scaleY = 20;
parallelepiped4.scaleZ = 12;
rightWall.Shape = parallelepiped4;

Dynamo.SceneBox = new Box(0, 40, 0, 40, 0, 25);
Box bx = Dynamo.SceneBox;
Dynamo.SceneDrawShape(true);
int iTotalRes = 0;

for (int i = 0; i < 1500; i++)
{
    Dynamo.SceneDrawShape(true);
    if (i % 5 == 0 && iTotalRes < 100)
    {
        iTotalRes++;
    }
    System.Threading.Thread.Sleep(10);

    double oscillationAmplitude = 0.2;

    for (int j = 0; j < numBubbles; j++)
    {
        bubbles[j].x += oscillationAmplitude * Math.Cos(Math.PI * random.NextDouble());
        bubbles[j].y += oscillationAmplitude * Math.Cos(Math.PI * random.NextDouble());
        bubbles[j].z += bubbles[j].v_z * DT;
        if (bubbles[j].z > serface_lavel + bubbles[j].radius)
        {
            bubbles[j].x = initial_x[j];
            bubbles[j].y = initial_y[j];
            bubbles[j].z = initial_z[j];
            bubbles[j].v_z = 0;
        }
        bubbles[j].v_z += g * DT * random.NextDouble();
    }

}
