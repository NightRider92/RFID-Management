import { Component } from '@angular/core'
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.style.css', '../../styles.css']
})
export class UsersComponent {

  // Public variables
  public response: any[] = [];

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {
    this.fetchData();
  }

  // Fetch data from the server
  private fetchData() {
    this.http.get<any[]>('https://localhost:7274/api/v1/database/request-all-data', { responseType: 'json' }).subscribe(data => {
      this.response = data;
    });
  }

  // Add new user
  public addUser() {

    let name = prompt("Please enter the user name");
    if (!name || name.length <= 0) {
      return;
    }

    let rfidVal = prompt("Please enter the RFID value");
    if (!rfidVal || rfidVal.length <= 0) {
      return;
    }

    let table = document.getElementById("users-table");
    if (!table) {
      console.log("Unable to find table object");
      return;
    }

    var body = "{\"username\": \"" + name + "\",  \"value\":\"" + rfidVal + "\", \"createdTime\": \"" + new Date().toLocaleString() + "\"}";
    console.log(body);

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    this.http.post('https://localhost:7274/api/v1/database/insert-data', body, httpOptions).subscribe(data => {
      console.log(data);

      // Reload the current route
      this.route.params.subscribe(params => {
        const currentUrl = this.router.url;
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate([currentUrl]);
        });
      });

      this.fetchData();
    });
  }

  // Remove user
  public removeUser(val: string) {
    let isExecuted = confirm("Are you sure to execute this action?");
    if (isExecuted) {

      let row = document.getElementById("row" + val);
      if (!row) {
        console.log("Unable to find row object");
        return;
      }

      this.http.delete('https://localhost:7274/api/v1/database/remove-data?id=' + val + '', { responseType: 'json' }).subscribe(data => {
        console.log(data);
      });
      row.parentNode?.removeChild(row);
    }
  }
}
