import { Component, ElementRef, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-page-structure',
  templateUrl: './page-structure.component.html',
  styleUrls: ['./page-structure.component.css']
})
export class PageStructureComponent implements OnInit {

  constructor() { }

  @Input('right-menu') rightMenu: TemplateRef<any> | null = null
  @Input('left-menu') leftMenu: TemplateRef<any> | null = null
  @Input('main') main: TemplateRef<any> | null = null

  ngOnInit() {
  }

}
