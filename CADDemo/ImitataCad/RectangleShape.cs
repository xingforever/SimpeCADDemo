﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImitataCad
{
    [Serializable]
    public partial class RectangleShape : BaseShape
    {
        Rectangle rec = new Rectangle();
        public void drawRec(Point p1, Point p2)
        {
            Point p = new Point();
            if (p1.X < p2.X && p1.Y < p2.Y)
            {
                rec = new Rectangle(p1, getSize(p1, p2));
            }
            if (p1.X < p2.X && p1.Y > p2.Y)
            {
                p.X = p1.X;
                p.Y = p2.Y;
                rec = new Rectangle(p, getSize(p1, p2));
            }
            if (p1.X > p2.X && p1.Y > p2.Y)
            {
                rec = new Rectangle(p2, getSize(p1, p2));
            }
            if (p1.X > p2.X && p1.Y < p2.Y)
            {
                p.X = p2.X;
                p.Y = p1.Y;
                rec = new Rectangle(p, getSize(p1, p2));
            }

        }

        public Size getSize(Point p1, Point p2)
        {
            int iWidth = Math.Abs(p1.X - p2.X);
            int iHeight = Math.Abs(p1.Y - p2.Y);
            return new Size(iWidth, iHeight);
        }

        public bool isInRectangle(Point p1, Point p2, Point testPoint)
        {
            drawRec(p1, p2);
            return rec.Contains(testPoint);
        }

        public override bool catchShape(Point testPoint)
        {
            return isInRectangle(this.getP1(), this.getP2(), testPoint);
        }

        public override void draw(Graphics g, Color c)
        {
            drawRec(getP1(), getP2());
            g.DrawRectangle(BaseShape.getPen(c), rec);

        }

        public override Point[] getAllHitPoint()
        {
            Point[] allHitPoint = new Point[4];
            allHitPoint[0] = new Point(rec.Left, rec.Top);
            allHitPoint[1] = new Point(rec.Right, rec.Top);
            allHitPoint[2] = new Point(rec.Right, rec.Bottom);
            allHitPoint[3] = new Point(rec.Left, rec.Bottom);
            return allHitPoint;
        }

        public override void setHitPoint(int hitPointIndex, Point newPoint)
        {
            int left = rec.Left; int right = rec.Right; int top = rec.Top; int bottom = rec.Bottom;
            switch (hitPointIndex)
            {
                case 0:
                    {
                        left += newPoint.X;
                        right += newPoint.X;
                        top += newPoint.Y;
                        bottom += newPoint.Y;
                        break;
                    }
                case 1:
                    top = newPoint.Y;
                    left = newPoint.X;
                    break;

                case 2:
                    if (newPoint.Y < bottom)
                    {
                        top = newPoint.Y;
                    }
                    else
                    {
                        bottom = newPoint.Y;
                    }
                    break;
                case 3:
                    top = newPoint.Y;
                    right = newPoint.X;
                    break;
                case 4:
                    right = newPoint.X;
                    break;
                case 5:
                    bottom = newPoint.Y;
                    right = newPoint.X;
                    break;
                case 6:
                    int i = top;
                    if (newPoint.Y > top)
                    {
                        bottom = newPoint.Y;
                    }
                    else
                    {
                        top = newPoint.Y;
                    }
                    break;
                case 7:
                    bottom = newPoint.Y;
                    left = newPoint.X;
                    break;
                case 8:
                    left = newPoint.X;
                    break;

            }
            SetRectangle(left, top, right - left, bottom - top);
            Point temp = new Point();
            temp.X = rec.X;
            temp.Y = rec.Y;
            this.setP1(temp);
            temp = getP1() + rec.Size;
            this.setP2(temp);
        }

        protected void SetRectangle(int x, int y, int width, int height)
        {
            rec.X = x;
            rec.Y = y;
            rec.Width = width;
            rec.Height = height;
        }

        //重写复制方法
        public override BaseShape copySelf()
        {
            RectangleShape copyRetangleShape = new RectangleShape();
            //复制起点
            copyRetangleShape.setP1(this.getP1());
            //复制终点
            copyRetangleShape.setP2(this.getP2());
            return copyRetangleShape;
        }
    }
}
