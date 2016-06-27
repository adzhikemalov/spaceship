using Assets.Scripts.World.WorldObjects;
using Assets.Utils;
using Assets.World.Map;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ShipEditor : EditorWindow
{

    public enum GUIState
    {
        DrawGridParams,
        DrawSelectedCellParams,
        DrawMultipleSelectedCellParam
    };

    public const string DestinationPath = "Ships";

    private GUIState _state = GUIState.DrawGridParams;
    private Texture2D _texture;
    private string _name;
    private int _canvasX = 220;
    private int _canvasY = 10;
    private int _canvasWidth = 640;
    private int _canvasHeight = 480;
    private Vector2 _mousePos;
    private float _dragOffsetX = float.NaN;
    private float _dragOffsetY = float.NaN;
    private bool _isDragingGrid = false;
    private List<Vector2> _selectedCells = new List<Vector2>();
    [SerializeField]
    private List<CellModel> _cells = new List<CellModel>();

    [MenuItem("ShipEditor/OpenEditor")]
    public static void OpenEditor()
    {
        var instance = EditorWindow.GetWindowWithRect<ShipEditor>(new Rect(0, 0, 870, 600), false, "Ship Editor");
        instance.ClearCells();
        instance.UpdateCells();
    }

    public void SaveShip()
    {
        Debug.Log(_name);
        var map = new MapModel();
        map.CellSize = _cellSize;
        map.Cols = _gridCols;
        map.Rows = _gridRows;
        map.Cells = _cells;
        var ship = new ShipData();
        ship.Map = map;
        var str = JsonUtility.ToJson(ship);
        File.WriteAllText(@Application.dataPath + "/" + DestinationPath + "/" + _name + ".txt", str.ToString());
    }

    public void LoadShip()
    {
        var path = EditorUtility.OpenFilePanel("Open ship file", @Application.dataPath + "/" + DestinationPath, "txt");
        if (!string.IsNullOrEmpty(path))
        {
            _name = Path.GetFileNameWithoutExtension(path);
            var temp = File.ReadAllText(path);
            var ship = JsonUtility.FromJson<ShipData>(temp);
            _cellSize = ship.Map.CellSize;
            _gridRows = ship.Map.Rows;
            _gridCols = ship.Map.Cols;
            _cells = ship.Map.Cells;
        }
    }

    public void ClearCells()
    {
        _selectedCells.Clear();
        _cells.Clear();
    }

    void OnGUI()
    {
        if (_selectedCells.Count == 1)
        {
            _state = GUIState.DrawSelectedCellParams;
        }
        else if (_selectedCells.Count > 1)
        {
            _state = GUIState.DrawMultipleSelectedCellParam;
        }
        else
        {
            _state = GUIState.DrawGridParams;
        }

        UpdateCells();
        DrawUI();


        Event e = Event.current;
        _mousePos = e.mousePosition;
        switch (e.type)
        {
            case EventType.keyDown:
                {
                    if (Event.current.keyCode == (KeyCode.A))
                    {

                    }
                    break;
                }
        }

        if (e.button == 1)
        {
            if (_gridRect.Contains(_mousePos))
            {
                _isDragingGrid = true;
            }
        }
        else
        {
            _isDragingGrid = false;
            _dragOffsetX = float.NaN;
        }
        if (e.button == 0 && e.type == EventType.MouseDown)
        {
            if (_gridRect.Contains(_mousePos))
            {
                var cellPos = GetCellPosition(_mousePos.x, _mousePos.y);
                if (!_selectedCells.Contains(cellPos)) _selectedCells.Add(cellPos);
                else _selectedCells.Remove(cellPos);
            }
        }

        if (_isDragingGrid)
        {
            DragGrid();
        }

        DrawGrid();
    }

    private float _gridX = 0;
    private float _gridY = 0;
    private int _gridCols = 5;
    private int _gridRows = 5;
    private int _cellSize = 20;
    private Rect _gridRect;
    private List<CellModel> _tempList;
    private void UpdateCells()
    {
        if (_cells.Count == _gridCols * _gridRows)
            return;
        _tempList = new List<CellModel>(_cells);
        _cells.Clear();
        for (int i = 0; i < _gridCols; i++)
        {
            for (int j = 0; j < _gridRows; j++)
            {
                if (!CellExist(i, j))
                    _cells.Add(new CellModel(i, j));
                else
                    ReturnToCells(i, j);
            }
        }
        _tempList.Clear();
    }

    private void ReturnToCells(int col, int row)
    {
        for (int i = 0; i < _tempList.Count; i++)
        {
            if (_tempList[i].Position.x == col && _tempList[i].Position.y == row)
                _cells.Add(_tempList[i]);
        }
    }

    private bool CellExist(int col, int row)
    {
        for (int i = 0; i < _tempList.Count; i++)
        {
            if (_tempList[i].Position.x == col && _tempList[i].Position.y == row)
                return true;
        }
        return false;
    }


    private Vector2 GetCellPosition(float x, float y)
    {
        var result = new Vector2();
        result.x = (int)((x - _gridX) / _cellSize);
        result.y = (int)((y - _gridY) / _cellSize);
        return result;
    }

    private Vector2 GetActualCellPosition(Vector2 position)
    {
        var result = new Vector2();
        result.x = position.x * _cellSize + _gridX;
        result.y = position.y * _cellSize + _gridY;
        return result;
    }

    private void DrawUI()
    {
        GUILayoutOption[] options = { GUILayout.Width(100f) };
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical(options);
        _name = EditorGUILayout.TextField("Ship Name", _name);
        switch (_state)
        {
            case GUIState.DrawGridParams:
                {
                    _texture = (Texture2D)EditorGUILayout.ObjectField("Open texture", _texture, typeof(Texture2D), false);
                    int.TryParse(EditorGUILayout.TextField("Grid Cols", _gridCols.ToString()), out _gridCols);
                    int.TryParse(EditorGUILayout.TextField("Grid Rows", _gridRows.ToString()), out _gridRows);
                    int.TryParse(EditorGUILayout.TextField("Cell size", _cellSize.ToString()), out _cellSize);
                    float gridPosX;
                    float gridPosY;
                    float.TryParse(EditorGUILayout.TextField("Grid X", GridPositionX.ToString()), out gridPosX);
                    float.TryParse(EditorGUILayout.TextField("Grid Y", GridPositionY.ToString()), out gridPosY);
                    GridPositionX = gridPosX;
                    GridPositionY = gridPosY;
                    break;
                }
            case GUIState.DrawSelectedCellParams:
                {
                    EditorGUILayout.Space();
                    var layout = EditorGUILayout.BeginHorizontal();
                    layout.x = 10;
                    var selectedCell = _selectedCells[0];
                    var selectedCellModel = GetCellModel(selectedCell.x, selectedCell.y);
                    if (GUI.Button(new Rect(new Vector2(layout.x + 0, layout.y + 25), new Vector2(25, 25)), "<"))
                    {
                        selectedCellModel.Walls[(int)WallPosition.Left] = !selectedCellModel.Walls[(int)WallPosition.Left];
                    }
                    if (GUI.Button(new Rect(new Vector2(layout.x + 25, layout.y + 0), new Vector2(25, 25)), "^"))
                    {
                        selectedCellModel.Walls[(int)WallPosition.Top] = !selectedCellModel.Walls[(int)WallPosition.Top];
                    }
                    if (GUI.Button(new Rect(new Vector2(layout.x + 25, layout.y + 25), new Vector2(25, 25)), "X"))
                    {
                        selectedCellModel.Enabled = !selectedCellModel.Enabled;
                    }
                    if (GUI.Button(new Rect(new Vector2(layout.x + 50, layout.y + 25), new Vector2(25, 25)), ">"))
                    {
                        selectedCellModel.Walls[(int)WallPosition.Right] = !selectedCellModel.Walls[(int)WallPosition.Right];
                    }
                    if (GUI.Button(new Rect(new Vector2(layout.x + 25, layout.y + 50), new Vector2(25, 25)), "v"))
                    {
                        selectedCellModel.Walls[(int)WallPosition.Bottom] = !selectedCellModel.Walls[(int)WallPosition.Bottom];
                    }
                    EditorGUILayout.EndHorizontal();
                    break;
                }
            case GUIState.DrawMultipleSelectedCellParam:
                {
                    EditorGUILayout.Space();
                    var layout = EditorGUILayout.BeginHorizontal();
                    layout.x = 10;
                    if (GUI.Button(new Rect(new Vector2(layout.x + 25, layout.y + 25), new Vector2(25, 25)), "x"))
                    {
                        foreach (var selectedCell in _selectedCells)
                        {
                            var selectedCellModel = GetCellModel(selectedCell.x, selectedCell.y);
                            selectedCellModel.Enabled = !selectedCellModel.Enabled;
                        }
                    }
                    break;
                }
        }

        if (_texture)
            EditorGUI.DrawPreviewTexture(new Rect(_canvasX, _canvasY, _canvasWidth, _canvasHeight), _texture);
        EditorGUILayout.Space();
        if (GUI.Button(new Rect(new Vector2(0, 500), new Vector2(100, 50)), "SAVE"))
        {
            SaveShip();
        }
        if (GUI.Button(new Rect(new Vector2(110, 500), new Vector2(100, 50)), "LOAD"))
        {
            LoadShip();
        }
        EditorGUILayout.EndVertical();
    }

    public float GridPositionX
    {
        get { return _gridX - _canvasX; }
        set { _gridX = value + _canvasX; }
    }

    public float GridPositionY
    {
        get { return _gridY - _canvasY; }
        set { _gridY = value + _canvasY; }
    }

    private void DrawGrid()
    {
        if (_gridX < _canvasX)
            _gridX = _canvasX;
        if (_gridY < _canvasY)
            _gridY = _canvasY;
        if (_gridX + _gridCols * _cellSize > _canvasX + _canvasWidth)
        {
            _gridX = _canvasX + _canvasWidth - _gridCols * _cellSize;
        }
        if (_gridY + _gridRows * _cellSize > _canvasY + _canvasHeight)
        {
            _gridY = _canvasY + _canvasHeight - _gridRows * _cellSize;
        }


        for (int i = 0; i <= _gridCols; i++)
        {
            var from = new Vector3(_gridX + i * _cellSize, _gridY);
            var to = new Vector2(_gridX + i * _cellSize, _gridY + _gridRows * _cellSize);
            Handles.DrawLine(from, to);
        }
        for (int i = 0; i <= _gridRows; i++)
        {
            var from = new Vector2(_gridX, _gridY + i * _cellSize);
            var to = new Vector2(_gridX + _gridCols * _cellSize, _gridY + i * _cellSize);
            Handles.DrawLine(from, to);
        }
        _gridRect = new Rect(_gridX, _gridY, _gridCols * _cellSize, _gridRows * _cellSize);

        foreach (var item in _selectedCells)
        {
            var actualPos = GetActualCellPosition(item);
            Handles.DrawSolidRectangleWithOutline(new Rect(actualPos.x, actualPos.y, _cellSize, _cellSize), Color.green, Color.cyan);
        }
        foreach (var cell in _cells)
        {
            if (!cell.Enabled)
            {
                var actualPos = GetActualCellPosition(new Vector2(cell.Position.x, cell.Position.y));
                Handles.color = new Color(1, 1, 1, 0.5f);
                Handles.DrawSolidRectangleWithOutline(new Rect(actualPos.x, actualPos.y, _cellSize, _cellSize), Color.red, Color.white);
            }
            DrawWalls(cell);
        }
        Repaint();
    }

    private CellModel GetCellModel(float x, float y)
    {
        var position = new Point(x, y);
        foreach (var item in _cells)
        {
            if (item.Position.Equals(position))
                return item;
        }
        return null;
    }

    private void DrawWalls(CellModel cell)
    {
        Vector2 from, to;
        Handles.color = Color.black;
        if (cell.Walls[(int)WallPosition.Left])
        {
            from = new Vector2(cell.Position.x * _cellSize + _gridX, cell.Position.y * _cellSize + _gridY);
            to = new Vector2(cell.Position.x * _cellSize + _gridX, cell.Position.y * _cellSize + _gridY + _cellSize);
            Handles.DrawLine(from, to);
        }
        if (cell.Walls[(int)WallPosition.Top])
        {
            from = new Vector2(cell.Position.x * _cellSize + _gridX, cell.Position.y * _cellSize + _gridY);
            to = new Vector2(cell.Position.x * _cellSize + _gridX + _cellSize, cell.Position.y * _cellSize + _gridY);
            Handles.DrawLine(from, to);
        }
        if (cell.Walls[(int)WallPosition.Right])
        {
            from = new Vector2(cell.Position.x * _cellSize + _gridX + _cellSize, cell.Position.y * _cellSize + _gridY);
            to = new Vector2(cell.Position.x * _cellSize + _gridX + _cellSize, cell.Position.y * _cellSize + _gridY + _cellSize);
            Handles.DrawLine(from, to);
        }
        if (cell.Walls[(int)WallPosition.Bottom])
        {
            from = new Vector2(cell.Position.x * _cellSize + _gridX, cell.Position.y * _cellSize + _gridY + _cellSize);
            to = new Vector2(cell.Position.x * _cellSize + _gridX + _cellSize, cell.Position.y * _cellSize + _gridY + _cellSize);
            Handles.DrawLine(from, to);
        }
    }


    private void DragGrid()
    {
        if (float.IsNaN(_dragOffsetX))
        {
            _dragOffsetX = _mousePos.x - _gridX;
            _dragOffsetY = _mousePos.y - _gridY;
        }
        _gridX = _mousePos.x - _dragOffsetX;
        _gridY = _mousePos.y - _dragOffsetY;
    }
}
