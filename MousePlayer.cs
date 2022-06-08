using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MouseFinderReloaded
{
	public class MousePlayer : ModPlayer
	{
		private static readonly Texture2D _texture = Main.cursorRadialTexture;

		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (Main.netMode != NetmodeID.Server && !Main.gameMenu && !Main.ingameOptionsWindow)
			{
				var playerCenter = new Vector2(player.Center.X - Main.screenPosition.X, player.Center.Y - Main.screenPosition.Y);
				var linePos = new Vector2();

				var max = 96;
				var start = 32;
				var distance = Vector2.Distance(player.Center, Main.MouseWorld);
				var angle = (float) Math.Atan2(player.Center.Y - Main.MouseWorld.Y, player.Center.X - Main.MouseWorld.X);
				if (Config.Instance.DrawLineTowardCursor)
				{
					var alpha = 1f;
					if (Main.ThickMouse)
					{
						for (var index = 0; index < distance - 2; index += (int) (4 * 0.5f))
						{
							alpha = MathHelper.Clamp(((float) (index - start) / (float) (max)), 0, 1) * 2;
							linePos.X = (float) (Math.Cos(angle) * -(index) + playerCenter.X);
							linePos.Y = (float) (Math.Sin(angle) * -(index) + playerCenter.Y);
							Main.spriteBatch.Draw(Main.hbTexture1, linePos, new Rectangle(4, 2, 4, Main.hbTexture1.Height - 4), Main.MouseBorderColor * alpha * Config.Instance.Alpha,
								angle, new Vector2(6f, 4f), 0.5f, SpriteEffects.None, 0f);
						}

						linePos.X = (float) (Math.Cos(angle) * -(distance - 2) + playerCenter.X);
						linePos.Y = (float) (Math.Sin(angle) * -(distance - 2) + playerCenter.Y);
						Main.spriteBatch.Draw(Main.hbTexture1, linePos, new Rectangle(0, 2, 4, Main.hbTexture1.Height - 4), Main.MouseBorderColor * alpha * Config.Instance.Alpha,
							angle, new Vector2(6f, 4f), 0.5f, SpriteEffects.None, 0f);
					}

					for (var index = 0; index < distance - 2; index += (int) (4 * 0.5f))
					{
						alpha = MathHelper.Clamp(((float) (index - start) / (float) (max)), 0, 1) * 2;
						linePos.X = (float) (Math.Cos(angle) * -(index) + playerCenter.X);
						linePos.Y = (float) (Math.Sin(angle) * -(index) + playerCenter.Y);
						Main.spriteBatch.Draw(Main.hbTexture2, linePos, new Rectangle(4, 4, 4, Main.hbTexture2.Height - 8), Main.cursorColor * alpha * Config.Instance.Alpha, angle,
							new Vector2(6f, 2f), 0.5f, SpriteEffects.None, 0f);
					}

					linePos.X = (float) (Math.Cos(angle) * -(distance - 2) + playerCenter.X);
					linePos.Y = (float) (Math.Sin(angle) * -(distance - 2) + playerCenter.Y);
					Main.spriteBatch.Draw(Main.hbTexture2, linePos, new Rectangle(0, 4, 4, Main.hbTexture2.Height - 8), Main.cursorColor * alpha * Config.Instance.Alpha, angle,
						new Vector2(6f, 2f), 0.5f, SpriteEffects.None, 0f);
				}

				if (Config.Instance.DrawCrosshair)
				{
					if (Main.ThickMouse)
					{
						for (var index = 0; index < Main.screenHeight; index += (int) ((Main.hbTexture1.Width - 8) * 0.5f))
						{
							linePos.X = Main.MouseWorld.X - Main.screenPosition.X;
							linePos.Y = index;
							Main.spriteBatch.Draw(Main.hbTexture1, linePos, new Rectangle(4, 2, Main.hbTexture1.Width - 8, Main.hbTexture1.Height - 4),
								Main.MouseBorderColor * Config.Instance.Alpha, (float) (Math.PI * 90f / 180.0), new Vector2(6f, 4f), 0.5f, SpriteEffects.None, 0f);
						}

						for (var index = 0; index < Main.screenWidth; index += (int) ((Main.hbTexture1.Width - 8) * 0.5f))
						{
							linePos.X = index;
							linePos.Y = Main.MouseWorld.Y - Main.screenPosition.Y;
							Main.spriteBatch.Draw(Main.hbTexture1, linePos, new Rectangle(4, 2, Main.hbTexture1.Width - 8, Main.hbTexture1.Height - 4),
								Main.MouseBorderColor * Config.Instance.Alpha, (float) (Math.PI * 0f / 180.0), new Vector2(6f, 4f), 0.5f, SpriteEffects.None, 0f);
						}
					}

					for (var index = 0; index < Main.screenHeight; index += (int) ((Main.hbTexture2.Width - 8) * 0.5f))
					{
						linePos.X = Main.MouseWorld.X - Main.screenPosition.X;
						linePos.Y = index;
						Main.spriteBatch.Draw(Main.hbTexture2, linePos, new Rectangle(4, 4, Main.hbTexture2.Width - 8, Main.hbTexture2.Height - 8),
							Main.cursorColor * Config.Instance.Alpha, (float) (Math.PI * 90f / 180.0), new Vector2(6f, 2f), 0.5f, SpriteEffects.None, 0f);
					}

					for (var index = 0; index < Main.screenWidth; index += (int) ((Main.hbTexture2.Width - 8) * 0.5f))
					{
						linePos.X = index;
						linePos.Y = Main.MouseWorld.Y - Main.screenPosition.Y;
						Main.spriteBatch.Draw(Main.hbTexture2, linePos, new Rectangle(4, 4, Main.hbTexture2.Width - 8, Main.hbTexture2.Height - 8),
							Main.cursorColor * Config.Instance.Alpha, (float) (Math.PI * 0f / 180.0), new Vector2(6f, 2f), 0.5f, SpriteEffects.None, 0f);
					}
				}

				if (Config.Instance.DrawRadialPointerTowardCursor)
				{
					distance = MathHelper.Clamp(distance * 0.5f, 0, max);
					linePos.X = (float) (Math.Cos(angle) * -(distance) + playerCenter.X);
					linePos.Y = (float) (Math.Sin(angle) * -(distance) + playerCenter.Y);
					var alpha = 1f;
					if (Main.ThickMouse)
					{
						alpha = MathHelper.Clamp(((float) (distance - start) / (float) (max)), 0, 1) * 0.5f;
						for (var index2 = 0; index2 < 4; ++index2)
						{
							Main.spriteBatch.Draw(Main.cursorTextures[11], linePos, new Rectangle(0, 0, Main.cursorTextures[11].Width, Main.cursorTextures[11].Height),
								Main.MouseBorderColor * alpha * Config.Instance.Alpha, angle - (float) (Math.PI * 45f / 180.0), new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
							Main.spriteBatch.Draw(Main.cursorTextures[11], linePos, new Rectangle(0, 0, Main.cursorTextures[11].Width, Main.cursorTextures[11].Height),
								Main.MouseBorderColor * alpha * Config.Instance.Alpha, angle - (float) (Math.PI * 45f / 180.0), new Vector2(2, 0), 1f, SpriteEffects.None, 0f);
							Main.spriteBatch.Draw(Main.cursorTextures[11], linePos, new Rectangle(0, 0, Main.cursorTextures[11].Width, Main.cursorTextures[11].Height),
								Main.MouseBorderColor * alpha * Config.Instance.Alpha, angle - (float) (Math.PI * 45f / 180.0), new Vector2(0, 2), 1f, SpriteEffects.None, 0f);
							Main.spriteBatch.Draw(Main.cursorTextures[11], linePos, new Rectangle(0, 0, Main.cursorTextures[11].Width, Main.cursorTextures[11].Height),
								Main.MouseBorderColor * alpha * Config.Instance.Alpha, angle - (float) (Math.PI * 45f / 180.0), new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
						}
					}

					alpha = MathHelper.Clamp(((float) (distance - start) / (float) (max)), 0, 1) * 2;
					Main.spriteBatch.Draw(Main.cursorTextures[0], new Vector2(linePos.X + 2, linePos.Y + 2),
						new Rectangle(0, 0, Main.cursorTextures[0].Width, Main.cursorTextures[0].Height), new Color(0f, 0f, 0f, 0.2f) * alpha * Config.Instance.Alpha,
						angle - (float) (Math.PI * 45f / 180.0), new Vector2(1, 1), 1f * 1.1f, SpriteEffects.None, 0f);
					Main.spriteBatch.Draw(Main.cursorTextures[0], linePos, new Rectangle(0, 0, Main.cursorTextures[0].Width, Main.cursorTextures[0].Height),
						Main.cursorColor * alpha * Config.Instance.Alpha, angle - (float) (Math.PI * 45f / 180.0), new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
				}

				if (Config.Instance.DrawRadialGridAroundCursor)
				{
					if (!_texture.IsDisposed)
						Main.spriteBatch.Draw(_texture, 
							new Vector2((float) Main.mouseX - _texture.Width / 2f, (float) Main.mouseY - _texture.Height / 2f) + Vector2.One, 
							null, Main.cursorColor, 0f, default, Main.cursorScale, SpriteEffects.None, 0f);
				}
			}
		}
	}
}