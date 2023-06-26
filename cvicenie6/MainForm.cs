/*
 * Created by SharpDevelop.
 * User: jacku
 * Date: 6. 10. 2022
 * Time: 13:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace cvicenie6
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		int[,] kamene = 
		{
		{2, 0, 0, 0, 0, 0, 0, 2},
		{0, 0, 0, 0, 0, 0, 0, 0},
		{0, 0, 0, 1, 1, 0, 0, 0},
		{0, 0, 1, 2, 2, 1, 0, 0},
		{0, 0, 1, 2, 2, 1, 0, 0},
		{0, 0, 0, 1, 1, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0},
		{2, 0, 0, 0, 0, 0, 0, 2}
		};
		
		Rectangle[,] Rectangles = new Rectangle[8, 8];
		bool selected = false;
		int x = -1;
		int y = -1;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			int x = 50;
			int y = 10;
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					Rectangles[i, j] = new Rectangle(x, y, 50, 50);
					x += 55;
				}
				x = 50;
				y += 55;
			}
		}
		
		void MainFormPaint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					if(kamene[i,j] == 0){
						g.FillRectangle(Brushes.White, Rectangles[i, j]);	
					}
					else if(kamene[i,j] == 1 || kamene[i,j] == 4){
						g.FillRectangle(Brushes.White, Rectangles[i, j]);
						g.FillEllipse(Brushes.Yellow, Rectangles[i, j].X, Rectangles[i, j].Y, 50, 50);
						if(kamene[i,j] == 4){
							g.DrawEllipse(Pens.Black, Rectangles[i,j].X, Rectangles[i,j].Y, 50, 50);
						}
						
					}
					else if (kamene[i,j] == 2){
						g.FillRectangle(Brushes.Red, Rectangles[i, j]);
					}
					
					else{
						g.FillRectangle(Brushes.Red, Rectangles[i, j]);
						g.FillEllipse(Brushes.Yellow, Rectangles[i, j].X, Rectangles[i, j].Y, 50, 50);
					}
						
				}
			}
	
		}
	
	
		bool validateTurn(int x, int y, int i, int j){
			if(x == i || y == j){
				if (x == i){
					int kontrolay = y;
					while(kontrolay != j){
						if(kamene[x, kontrolay] == 1 || kamene[x, kontrolay] == 3){
							return false;
						}
						if(y > j){
							kontrolay--;
						}
						else{
						kontrolay++;}
					}
					
				}
				else if (y == j){
					int kontrolax = x;
					while(kontrolax != i){
						if(kamene[kontrolax, y] == 1 || kamene[kontrolax, y] == 3){
							return false;
						}
						if(x > i){
							kontrolax--;
						}
						else{
						kontrolax++;}
					}
				}
				return true;
			}else{
				return false;
			}
		}
		
		void MainFormMouseClick(object sender, MouseEventArgs e)
		{
			
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					if (Rectangles[i,j].Contains(e.X, e.Y) && kamene[i,j]==1 && selected==false){
						kamene[i,j] = 4;
						selected = true;
						x = i;
						y = j;
						Invalidate();
					}
					else if (Rectangles[i,j].Contains(e.X, e.Y) && (kamene[i,j] == 2 || kamene[i,j] == 0 || kamene[i,j] == 3) && selected==false){
						
					}
					else if (Rectangles[i,j].Contains(e.X, e.Y) && kamene[i,j]==4){
						
					
							kamene[i,j] = 1;
							selected = false;
							Invalidate();
						
						
					}
					else if (Rectangles[i,j].Contains(e.X, e.Y) && kamene[i,j]!=1 && kamene[i,j]!=3){
						if (validateTurn(x,y,i,j) == false){
							break;
						}
						if(kamene[i,j] == 2){
							kamene[i,j] = 3;
							kamene[x,y] = 0;
							x = -1;
							y = -1;
							Invalidate();
						}
						else{
							kamene[i,j] = 1;
							kamene[x,y] = 0;
							x = -1;
							y = -1;
							Invalidate();
						}
						
						selected = false;
					}
				}
			
			}	
		}
	}
}
