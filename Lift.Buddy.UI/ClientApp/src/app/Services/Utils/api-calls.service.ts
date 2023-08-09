import { Injectable } from '@angular/core';
import { Response } from '../../Model/Response'

@Injectable({
  providedIn: 'root'
})
export class ApiCallsService {

  public jwtToken: string | null = "";
  public defaultUrl: string = "http://localhost:5200/";

  public async apiGet(url: string) {
    let response = new Response();
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
      const result = (await response.json()) as Response;
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

  public async apiPost(url: string, body: object) {
    let apiResponse = new Response();
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
        throw new Error(`Error on post call: ${response.status}`);
      }
      if (response.bodyUsed) {
        const result = (await response.json()) as Response;
        return result;
      }
      apiResponse.result = true;
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
