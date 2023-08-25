import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { treeData } from 'src/app/routes';

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
    this.navigationTreeData.data = treeData;
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

}
