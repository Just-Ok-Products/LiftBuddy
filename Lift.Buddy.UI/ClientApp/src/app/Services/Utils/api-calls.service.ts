import { Injectable } from '@angular/core';
import { Response } from '../../Model/Response'

@Injectable({
  providedIn: 'root'
})
export class ApiCallsService {

  public jwtToken: string = "";
  public defaultUrl: string = "http://localhost:5200/";

  public async apiGet<T>(url: string): Promise<Response<T>> {
    let response = new Response<T>();
    try {
      const response = await fetch(url,
        {
          method: 'GET',
          headers: {
            'Authorization': `Bearer ${this.jwtToken}`
          }
        });
      if (!response.ok) {
        throw new Error(`Error on post call: ${response.status}`);
      }
      const result = (await response.json()) as Response<T>;
      return result;
    } catch (error) {

      response.result = false;
      if (error instanceof Error) {
        console.error(`Error ${error.message}`)
        response.notes = error.message;
        return response;
      } else {
        console.error(`Unexpected error: `, error)
        response.notes = 'Unexpected error';
        return response;
      }
    }
  }

  public async apiPost<T>(url: string, body: object): Promise<Response<T>> {
    let apiResponse = new Response<T>();
    try {
      const response = await fetch(this.defaultUrl + url,
        {
          method: 'POST',
          body: JSON.stringify(body),
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${this.jwtToken}`
          }
        });
      if (!response.ok) {
        throw new Error(`Error on post call: ${response.statusText} ${response.status}`);
      }
      // TODO: add handling for empty body
      const result = (await response.json()) as Response<T>;
      return result;
    } catch (error) {

      apiResponse.result = false;
      if (error instanceof Error) {
        console.error(`Error ${error.message}`)
        apiResponse.notes = error.message;
      } else {
        console.error(`Unexpected error: `, error)
        apiResponse.notes = 'Unexpected error';
      }
    }
    return apiResponse;
  }
}
