import math
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from mpl_toolkits.mplot3d import Axes3D

cube = [];

for x in range(-1,2,2):
    for y in range(-1,2,2):
        for z in range(-1,2,2):
            cube.append([[x, y, z, 1]])

edges = [[1,2],[2,4],[4,3],[3,1],[1,5],[5,6],[6,2],[6,8],[8,4],[8,7],[7,3],[7,5]]

def mul(a, b):
    n = len(a[0])
    m = len(a)
    m2 = len(b[0])
    p = len(b)
    if m != m2:
        return -1;

    r = [[0 for x in range(n)] for y in range(p)]
    for i in range(n):
        for j in range(p):
            s = 0;
            for k in range(m):
                s += a[k][i] * b[j][k];
            r[j][i] = s;
    return r

def ident():
    return [[1,0,0,0],[0,1,0,0],[0,0,1,0],[0,0,0,1]]

def translate(mat, x, y, z):
    return mul(mat, [[1,0,0,0],[0,1,0,0],[0,0,1,0],[x,y,z,1]])

def rotate_z(mat, angle):
    sin = math.sin(angle)
    cos = math.cos(angle)

    return mul(mat, [[cos, sin, 0, 0],[ -sin, cos, 0, 0], [0,0,1,0], [0,0,0,1]])

def rotate_y(mat, angle):
    sin = math.sin(angle)
    cos = math.cos(angle)

    return mul(mat, [[cos, 0, -sin, 0], [0, 1, 0, 0],[sin, 0, cos, 0], [0,0,0,1]])

def rotate_x(mat, angle):
    sin = math.sin(angle)
    cos = math.cos(angle)

    return mul(mat, [[1, 0, 0, 0], [0, cos, sin, 0],[0, -sin, cos, 0], [0,0,0,1]])

def scale(mat, s):
    return mul(mat, [[s,0,0,0],[0,s,0,0],[0,0,s,0],[0,0,0,1]])

fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

#Set your values below
scale_value = 1/10
angle = -3.14 * 2
point = [1,1,1]


def animate(t):
    mat = ident()
    mat = translate(mat, *point)
    mat = scale(mat, scale_value**(t/100))
    mat = rotate_z(mat, t/100 * angle)
    mat = rotate_x(mat, t/100 * angle)
    mat = rotate_y(mat, t/100 * angle)
    mat = translate(mat, *point)

    cube_modif = [0] * 8

    for i in range(8):
        cube_modif[i] = mul(mat, cube[i])

    ax.clear()
    axis_scale = math.sqrt((math.sqrt(point[0]**2 + point[1]**2)**2 + point[2]**2) ) + 2 * scale_value + 1
    ax.set_xlim(-axis_scale + point[0], axis_scale + point[0])
    ax.set_ylim(-axis_scale + point[1], axis_scale + point[1])
    ax.set_zlim(-axis_scale + point[2], axis_scale + point[2])


    for edge in edges:
        xs = []
        ys = []
        zs = []
        xs.append(cube_modif[edge[0]-1][0][0])
        ys.append(cube_modif[edge[0]-1][0][1])
        zs.append(cube_modif[edge[0]-1][0][2])
        xs.append(cube_modif[edge[1]-1][0][0])
        ys.append(cube_modif[edge[1]-1][0][1])
        zs.append(cube_modif[edge[1]-1][0][2])
        ax.plot(xs, ys, zs)

ani = animation.FuncAnimation(fig, animate, interval=10, frames=100)
plt.show();
