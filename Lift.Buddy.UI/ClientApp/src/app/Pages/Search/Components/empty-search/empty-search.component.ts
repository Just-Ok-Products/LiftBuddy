import { Component, OnInit, ViewChild, ElementRef, TemplateRef } from '@angular/core';
import { UserData } from 'src/app/Model/UserData';

@Component({
  selector: 'app-empty-search',
  templateUrl: './empty-search.component.html',
  styleUrls: ['./empty-search.component.css']
})
export class EmptySearchComponent implements OnInit {

  public originalSuggested: UserData[] = [];
  public suggested: UserData[] = [];
  public searchValue: any = "";

  @ViewChild('input') input: ElementRef<HTMLInputElement> | undefined

  constructor() { }

  ngOnInit() {
    this.initSuggestedData();
  }

  private initSuggestedData() {
    let a = new UserData()
    a.username = 'mario';
    this.originalSuggested.push(a)
    this.suggested = this.originalSuggested.slice(10);
  }

  public updateSuggested() {
    this.suggested = this.originalSuggested.filter(x =>
      x.username.toLowerCase().includes(this.input?.nativeElement.value.toLocaleLowerCase() ?? '')
    );
  }

  public search() {
    console.log(this.input?.nativeElement.value)
  }

}
