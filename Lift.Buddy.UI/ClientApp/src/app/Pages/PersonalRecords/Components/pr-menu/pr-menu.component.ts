import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-pr-menu',
  templateUrl: './pr-menu.component.html',
  styleUrls: ['./pr-menu.component.css']
})

export class PrMenuComponent implements OnInit {

  @Output() onAddExercise: EventEmitter<void> = new EventEmitter();
  @Output() onSave: EventEmitter<void> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  public addExercise() {
    this.onAddExercise.emit();
  }

  public save() {
    this.onSave.emit();
  }
}
