using System;
using System.Drawing;
using System.Windows.Forms;
using MarsGame.Entity;
using MarsGame.Model;
using MarsGame.Texture;

namespace MarsGame
{
    public partial class MainForm : Form
    {
        private readonly Map _map;
        private readonly Player _player;
        private readonly ITextureContainer _textures;
        private readonly int _cellSize;

        private Control _playerTexture;
        private Control[,] _mapTextures;

        public MainForm(Map map, Player player, ITextureContainer texture, int cellSize)
        {
            _map = map;
            _player = player;
            _textures = texture;
            _cellSize = cellSize;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _player.OnMove +=
                (d) =>
                {
                    try
                    {
                        Invoke((MethodInvoker)DrawPlayer);
                    }
                    catch (InvalidOperationException) { }
                };

            DisplayMap();
            DisplayPlayer();
        }

        private void DisplayPlayer()
        {
            _playerTexture = new PictureBox()
            {
                Width = _cellSize,
                Height = _cellSize,
                Image = _textures.GetPlayerTexture(),
                Location = new Point(0, 0),
                BorderStyle = BorderStyle.FixedSingle
            };

            DrawPlayer();

            container.Controls.Add(_playerTexture);
            _playerTexture.BringToFront();
        }

        private void DrawPlayer()
        {
            _playerTexture.Location = new Point()
            {
                X = _player.X * _cellSize,
                Y = _player.Y * _cellSize
            };
        }

        private void DisplayMap()
        {
            container.Width = (int)((_map.Width + 0.55) * _cellSize);
            container.Height = (int)((_map.Height + 1.33) * _cellSize);
            container.Location = new Point(
                Width / 2 - container.Width / 2,
                Height / 2 - container.Height / 2);

            _mapTextures = new Control[_map.Width, _map.Height];

            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    Control box = CreateBlock(_map.Get(x, y));
                    _mapTextures[x, y] = box;
                    container.Controls.Add(box);
                }
            }
        }

        private Control CreateBlock(Cell cell)
        {
            PictureBox box = new PictureBox
            {
                Width = _cellSize,
                Height = _cellSize,
                Location = new Point
                {
                    X = cell.X * _cellSize,
                    Y = cell.Y * _cellSize
                },
                Image = _textures.GetTextureByBlockName(cell.Type),
                BorderStyle = BorderStyle.FixedSingle
            };

            return box;
        }
    }
}
