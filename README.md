# PromoCodeFactory

The `Promo Code Factory` system for issuing partner promo codes to customers grouped by their preferences.

# Description
The system maintains a customer database with their preferences (Family, Children, Theatre, Business, etc.) and allows partners to send promo codes to customers with selected preferences.
For example, promo codes for theatre tickets will be sent to customers with the "Theatre" preference.

Partners themselves decide which customer preferences should receive a given promo code, and they can do this in two ways: through a partner manager or via the API.
To send a promo code to customers, a manager can log in to the application's SPA interface and issue a promo code that was received from the partner in some way, for example by email. The promo code will be sent to all customers with a matching preference.

If a partner has the capability for software integration, they can issue promo codes by preference through the API.
To do this, they request the list of preferences from the API and pass the desired preference together with the promo code.
The functionality being developed does not include the distribution mechanism itself.

The system has two architecture options. For the MVP we consider a small monolithic PromoCodeFactory application with an API. There is also a microservices implementation, where the system is split into three microservices: Administration: Pcf.Administration, Receiving promo codes from partners: Pcf.ReceivingFromPartner, Offering promo codes to customers: Pcf.GivingToCustomer.

The Administration microservice is responsible for working with employees (partner managers) and roles. The Receiving promo codes from partners microservice is responsible for providing partners with an API to submit promo codes, as well as an API for managing partners.
The Offering promo codes to customers microservice is responsible for providing promo codes to specific customers and potentially for distributing them.

# Core Features
- A partner manager of our company can log in to the PromoCodeFactory web application, select a preference from the list, and issue a promo code received from the partner. After that, the promo code will be issued to all customers in the customer database who have that preference.
- A system administrator can:
  - log in to the application,
  - view the list of employees,
  - create a new employee or edit the data of an existing one;
  - view the number of promo codes issued by an employee in the employee's details;
  - issue promo codes, just like a manager;
  - cancel a partner's limit or set a new one.
- Administrators and employees must authenticate.
- A partner can create a new promo code for a preference through the API.
- So that a partner can issue promo codes by preference through the API, we provide access to the preferences reference list via the API.
- A partner's system can call our API, pass it a promo code along with a service description and a preference, after which our system will send it to the matching customers.
- The system provides a private CRUD API for working with the customer database. In the future, customer relationship managers will be able to populate the database through the interface, but for now this is only available via the API.
- There is also functionality for working with partners:
  - limits on promo code issuance are set for each partner,
  - if the limit is exceeded or has expired, the promo code cannot be issued.

# Overall MVP Architecture
![337460998-13b10f1b-d331-4ffb-ac2b-1d0cbf34b6de](https://github.com/user-attachments/assets/b3d5d6a5-20c5-40cb-afee-b16512382535)

# Overall Microservices Architecture
![337461037-a6c0d55a-6a84-470b-a67d-fef6b56d1104](https://github.com/user-attachments/assets/2bc2d6fe-5b0f-4e18-a943-c61f9e1efa28)

