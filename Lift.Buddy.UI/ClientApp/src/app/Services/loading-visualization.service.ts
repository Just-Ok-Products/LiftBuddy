import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Observer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingVisualizationService {

constructor() { }

  private isLoading = new BehaviorSubject<boolean>(false);
  public $isLoading = this.isLoading.asObservable();
  private _isLoading = false;

  public getIsLoading() {
    return this._isLoading;
  }

  public setIsLoading(loading: boolean) {
    this._isLoading = loading;
    this.isLoading.next(loading);
  }

}
