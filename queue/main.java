import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Dizikuyruk dizikuyruk = new Dizikuyruk();
        LinkedlistKuyruk linkedlistKuyruk = new LinkedlistKuyruk();

        String islemler ="islem 1 = dizi ile devam et" +
                "islem 2 = linkedlist ile devam et";

        Scanner scanner = new Scanner(System.in);
        System.out.println("bir islem secin.");
        String islem = scanner.nextLine();
        if(islem.equals("1")){
            String islemler1 = "islem 1= musteri ekle" +
                    "islem 2 = musteri sil" +
                    "islem q = cikis";
            while (true){
                System.out.println("bir islem seciniz.");
                String islem1 = scanner.nextLine();

                if(islem1.equals("1")){
                    System.out.println("musteri adi giriniz:");
                    String ad = scanner.nextLine();
                    Musteri musteri = new Musteri(ad);
                    System.out.println("oncelikli alan (1,2,3) : ");
                    String oncelik = scanner.nextLine();
                    if(oncelik.equals("1")){
                        dizikuyruk.ekle(musteri,1);
                    }
                    else if(oncelik.equals("2")){
                        dizikuyruk.ekle(musteri,2);
                    }
                    else if(oncelik.equals("3")){
                        dizikuyruk.ekle(musteri,3);
                    }else{
                        System.out.println("yanlis bir islem girdiniz.");
                    }
                } else if (islem1.equals("2")) {
                    dizikuyruk.siradanCikar();
                }else if (islem1.equals("q")){
                    System.out.println("programdan cikiliyor.");
                    break;
                }
                dizikuyruk.listele();
            }

        }
        else if (islem.equals("2")){
            String islemler2 = "islem 1= musteri ekle" +
                    "islem 2 = musteri sil" +
                    "islem q = cikis";
            while (true){
                System.out.println("bir islem seciniz.");
                String islem1 = scanner.nextLine();

                if(islem1.equals("1")){
                    System.out.println("musteri adi giriniz:");
                    String ad = scanner.nextLine();
                    Musteri musteri = new Musteri(ad);
                    System.out.println("oncelikli alan (1,2,3) : ");
                    String oncelik = scanner.nextLine();
                    if(oncelik.equals("1")){
                        linkedlistKuyruk.ekle(musteri,1);

                    }
                    else if(oncelik.equals("2")){
                        linkedlistKuyruk.ekle(musteri,2);
                    }
                    else if(oncelik.equals("3")){
                        linkedlistKuyruk.ekle(musteri,3);
                    }else{
                        System.out.println("yanlis bir islem girdiniz.");
                    }
                } else if (islem1.equals("2")) {
                    linkedlistKuyruk.siradanCikar();
                }else if (islem1.equals("q")){
                    System.out.println("programdan cikiliyor.");
                    break;
                }
                linkedlistKuyruk.listele();
            }
        }
        else {
            System.out.println("yanlis islem.");
        }
    }
}
