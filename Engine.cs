
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BarcodeGenerator
{
	public static class BarcodeEngine
	{
		public static byte[] GenerateBarcode(string fileName, string savePath, ImageFormat fileType, int width, int height, string barcodeString)
		{
			int barWidth = width / 65;
			int barHeight = height / 3;
			int gapWidth = barWidth / 2;

			Bitmap bitmap = new Bitmap(width + (gapWidth * 64), height, PixelFormat.Format32bppPArgb);

			if (barcodeString.Length > 65)
			{
				barcodeString = barcodeString.Substring(0, 64);
			}

			for (int i = 0; i < barcodeString.Length; i++)
			{
				int posX = i * (barWidth + gapWidth);

				int topY = 0;

				int midY = topY + barHeight;

				int bottomY = midY + barHeight;

				switch (barcodeString[i])
				{
					case 'A':
						DrawingBar(bitmap, posX, topY, barWidth, barHeight, CodePosition.Top);
						DrawingBar(bitmap, posX, midY, barWidth, barHeight, CodePosition.Mid);
						// DrawingBar(bitmap, bottomX, bottomY, barWidth, barWidth, CodePosition.Bottom);
						break;
					case 'D':
						//DrawingBar(bitmap, topX, topY, barWidth, barHeight, CodePosition.Top);
						DrawingBar(bitmap, posX, midY, barWidth, barHeight, CodePosition.Mid);
						DrawingBar(bitmap, posX, bottomY, barWidth, barHeight, CodePosition.Bottom);
						break;
					case 'F':
						DrawingBar(bitmap, posX, topY, barWidth, barHeight, CodePosition.Top);
						DrawingBar(bitmap, posX, midY, barWidth, barHeight, CodePosition.Mid);
						DrawingBar(bitmap, posX, bottomY, barWidth, barHeight, CodePosition.Bottom);
						break;
					case 'T':
					case 'S':
						//DrawingBar(bitmap, posX, topY, barWidth, barHeight, CodePosition.Top);
						DrawingBar(bitmap, posX, midY, barWidth, barHeight, CodePosition.Mid);
						// DrawingBar(bitmap, posX, bottomY, barWidth, barHeight, CodePosition.Bottom);
						break;
					default:
						DrawingBar(bitmap, posX, topY, barWidth, barHeight, CodePosition.Top);
						DrawingBar(bitmap, posX, midY, barWidth, barHeight, CodePosition.Mid);
						DrawingBar(bitmap, posX, bottomY, barWidth, barHeight, CodePosition.Bottom);
						break;
				}
			}

			bitmap.Save(string.Format($"{fileName}.{fileType.ToString()}"), fileType);

			using (var stream = new MemoryStream())
			{
				bitmap.Save(stream, fileType);
				return stream.ToArray();
			}

		}

		private static void DrawingBar(Bitmap bitmap, int positionX, int positionY, int width, int height, CodePosition codePosition)
		{
			Graphics graph = Graphics.FromImage(bitmap);
			Pen pen = new Pen(Color.White, 1);
			Rectangle topRect = new Rectangle(positionX, positionY, width, height);
			graph.DrawRectangle(pen, topRect);

			//SolidBrush brush;

			//switch (codePosition)
			//{
			//	case CodePosition.Top:
			//		brush = new SolidBrush(Color.Red);
			//		break;
			//	case CodePosition.Mid:
			//		brush = new SolidBrush(Color.Yellow);
			//		break;
			//	case CodePosition.Bottom:
			//		brush = new SolidBrush(Color.Green);
			//		break;
			//	default:
			//		brush = new SolidBrush(Color.Black);
			//		break;
			//}

			SolidBrush brush = new SolidBrush(Color.White);

			// Fill rectangle to screen.
			graph.FillRectangle(brush, topRect);
		}
	}

	public enum CodePosition
	{
		Top,
		Mid,
		Bottom
	}
}