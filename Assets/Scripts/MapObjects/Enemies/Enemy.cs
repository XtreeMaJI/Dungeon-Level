using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Character
{
    private const int PLAYER_DETECTION_RADIUS = 5; //Расстояние в клетках, при котором враг начнёт двигаться за игроком

    private Player player;

    public override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    public void MakeTurn()
    {
        //Если игрок находится на прилегающей клетке, атакуем его
        if (currentCell.surroundingCells.Contains(player.currentCell))
        {
            Attack(player);
            return;
        }

        //Ищем кратчайший путь до игрока
        List<HexCell> path = PathFinder.TryFindPath(currentCell, player.currentCell, PLAYER_DETECTION_RADIUS);

        //Двигаемся по найденному пути, если такой путь есть
        if(path.Count != 0)
        {
            TryMoveToCell(path.First());
        }

    }

    public override void OnZeroHealth()
    {
        Destroy(gameObject);
    }
}
