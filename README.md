Install Minikube:

For macOS:
Copy code
brew install minikube
For Windows:
Copy code
choco install minikube
For Linux, download the Minikube binary and install it manually.
Start Minikube:


minikube start
Create Namespaces:

kubectl create namespace namespace1
kubectl create namespace namespace2
Deploy YAML Files to Namespaces:


kubectl apply -f your_yaml_file.yaml -n namespace1
kubectl apply -f your_yaml_file.yaml -n namespace2
Replace your_yaml_file.yaml with the actual YAML file you want to deploy to the namespaces.

Ensure you have kubectl installed and configured to work with Minikube. You can install kubectl using a package manager or by downloading the binary from the Kubernetes release page.
