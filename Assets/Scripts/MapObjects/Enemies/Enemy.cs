using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Character
{
    private const int PLAYER_DETECTION_RADIUS = 5; //���������� � �������, ��� ������� ���� ����� ��������� �� �������

    private Player player;

    public override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    public void MakeTurn()
    {
        //���� ����� ��������� �� ����������� ������, ������� ���
        if (currentCell.surroundingCells.Contains(player.currentCell))
        {
            Attack(player);
            return;
        }

        //���� ���������� ���� �� ������
        List<HexCell> path = PathFinder.TryFindPath(currentCell, player.currentCell, PLAYER_DETECTION_RADIUS);

        //��������� �� ���������� ����, ���� ����� ���� ����
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
