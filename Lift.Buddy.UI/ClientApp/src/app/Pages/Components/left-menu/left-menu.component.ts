import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements OnInit {

  constructor() { }

  @Output() onClick: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.initTreeObject()
  }

  private initTreeObject() {
    let treeFlattener = new MatTreeFlattener(
      this.transformer,
      node => node.level,
      node => node.expandable,
      node => node.children,
    );

    this.navigationTreeData = new MatTreeFlatDataSource(this.navigationTreeControl, treeFlattener);
    this.navigationTreeData.data = this.treeData;
  }

  //#region Tree initialization
  public navigationTreeData: any
  private transformer = (node: any, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
      icon: node.icon,
      path: node.path
    };
  }
  public hasChild = (_: number, node: any) => node.expandable

  public navigationTreeControl: FlatTreeControl<any> = new FlatTreeControl<any>(
    node => node.level,
    node => !!node && node.children.length > 0
  )
  //#endregion

  public treeData = [
    {
      path: 'home',
      icon: 'home',
      name: 'home'
    },
    {
      name: 'PR section',
      children: [
        {
          name: 'pr',
          icon: 'dumbbell',
          path: 'pr'
        }
      ]
    },
    {
      name: 'Workout Plans',
      children: [
        {name: 'workouts', icon: 'calendar', path: 'workouts/home'},
        {name: 'add workout', icon: 'pen', path: 'workouts/add/-1'}
      ]
    },
  ]

}
